using System.Net;
using System.Net.Http.Headers;
using InfoVeriti.Libs.Network.Abstracts.HttpClient;
using InfoVeriti.Libs.Network.Abstracts.WebApi;
using InfoVeriti.Libs.Network.Abstracts.WebClient;
using InfoVeriti.WebApi.Contracts.Auth;
using InfoVeriti.WebApi.Facade.Abstracts;
using InfoVeriti.WebApi.Facade.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace InfoVeriti.WebApi.Facade;

public class InfoVeritiWebApiInterceptor: IWebClientInterceptor
{
	private readonly Config _config;
	private readonly IAuthStore _authStore;
	private readonly IApiClient _apiClient;
	private readonly IHttpClient _http;

	public InfoVeritiWebApiInterceptor(IServiceProvider serviceProvider )
	{
		_config = serviceProvider.GetRequiredService<Config>();
		_authStore = serviceProvider.GetRequiredService<IAuthStore>();
		_apiClient = serviceProvider.GetRequiredService<IApiClient>();
		_http = serviceProvider.GetRequiredService<IHttpClient>();
	}


	public HttpResponseMessage OnIntercept( WebClientOnInterceptParameters parameters )
	{
		var response = parameters.Response;
		var statusCode = response.StatusCode;

		if ( statusCode == HttpStatusCode.Unauthorized )
		{
			var tsk = response.Content.ReadAsStringAsync();
			tsk.Wait();
			var bodyText = tsk.Result;
			if ( !string.IsNullOrEmpty( tsk.Result ) && bodyText.Contains( "Token Expired" ) )
			{
				var oldAuth = _authStore.Load();

				var newAuth = RefreshAuth( oldAuth ?? new AuthResponse() );
				
				if (newAuth.IsInvalid)
					throw new Exception("Refresh Auth failed");
				
				_authStore.Save( newAuth );
				
 
				var request = new HttpRequestMessage( parameters.Request.Method, parameters.Request.RequestUri );
				request.Content = parameters.Request.Content;
				request.Headers.Authorization = new AuthenticationHeaderValue( "Bearer", newAuth.Token );
				//todo: add auth headers to request (see: IAuthData) 
				
				var task = _http.SendAsync( request );
				task.Wait();
				return task.Result;
			}

		}

		return response;
	}
	
	
	private AuthResponse RefreshAuth(AuthResponse oldAuth)
	{

		AuthResponse? result = null;
		var refreshRequest = new AuthRefreshRequest(oldAuth.ClientHash ?? string.Empty);

		var wait = true;
		Exception? ex = null;
		
		_apiClient
			.WithUrl( "/Auth/Token" )
			.WithBody( refreshRequest )
			.Post<AuthResponse>()
			.Subscribe( ( next ) =>
			{
				if ( next != null && !string.IsNullOrEmpty( next.Token ) && !string.IsNullOrEmpty( next.ClientHash ) && next.ExpiresAt != null )
				{
					result = new AuthResponse() { Token = next.Token, ClientHash = next.ClientHash, ExpiresAt = next.ExpiresAt };
					
				}
				wait = false;
			}, ( error ) =>
			{
				ex = new Exception( error.GetHttpStatusMessage(), error );
				wait = false;
			}, () =>
			{
				wait = false;
			} );
		
		while ( wait )
			Task.Delay( 100 ).Wait();
		
		if ( ex != null )
			throw ex;
		
		if (result == null)
			throw new Exception("Refresh Auth failed");

		return result;
	}

}