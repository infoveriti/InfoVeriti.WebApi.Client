using InfoVeriti.Libs.Network.Abstracts.WebApi;
using InfoVeriti.WebApi.Contracts.Auth;
using InfoVeriti.WebApi.Extensions;
using InfoVeriti.WebApi.Facade.Extensions;

namespace InfoVeriti.WebApi.Facade.Items;

internal class LoginHelper
{
	private readonly IApiClient _apiClient;

	private LoginHelper( IApiClient apiClient )
	{
		_apiClient = apiClient;
	}

	internal static LoginHelper CreateInstance( IApiClient apiClient )
	{
		return new LoginHelper( apiClient );
	}

	public AuthResponse Login(Func<string?> getLogin, Func<string?> getPassword)
	{
		var login = getLogin()?.Trim(Environment.NewLine.ToCharArray()).Trim() ?? string.Empty;
		var password = getPassword()?.Trim( Environment.NewLine.ToCharArray() ).Trim().ToMd5() ?? string.Empty;
		
		if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
			throw new Exception( "Login or password is empty" );

		var wait = true;
		AuthResponse? result = null;
		Exception? error = null;
		
		_apiClient
				.WithUrl( "/Auth" )
				.WithBody( new AuthRequest( login, password ) )
			.Post<AuthResponse>( ).Subscribe( response =>
				{
					if ( response.IsError )
						error = new Exception( response.Error );
					else
						result = response;
								
					wait = false;

					if ( result is not null )
					{
						Console.WriteLine( "Login to API: Info Veriti WebAPI" );
						Console.WriteLine( "      User Name: [{0}]", login );
						Console.WriteLine( "  Password hash: [{0}]", password );
						Console.WriteLine( "          Token: [{0}]", result.Token );
						Console.WriteLine( "    Client Hash: [{0}]", result.ClientHash );
					}
					else
						Console.WriteLine( "Login failed" );

				}, ( err ) =>
				{
					error = new Exception( err.GetHttpStatusMessage(), err );
					wait = false;
				}
				, () 
					=> wait = false 
				);
		
		while ( wait )
			Task.Delay( 100 ).Wait();
		
		if (error is not null)
			throw error;
		
		if (result is null)
			throw new Exception( "No response from server" );
		
		return result;
		
	}
}