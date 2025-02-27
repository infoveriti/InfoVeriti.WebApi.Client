using InfoVeriti.Libs.Network.Abstracts.Data;
using InfoVeriti.Libs.Network.Abstracts.WebClient;
using Microsoft.Extensions.DependencyInjection;

namespace InfoVeriti.WebApi.Facade.Requests;

public class AuthRequestBuilder: IRequestBuilder
{
	internal static decimal TimeDifference = decimal.Zero;
	
	private readonly IAuthData _authData;

	public AuthRequestBuilder(IServiceProvider serviceProvider)
	{
		_authData = serviceProvider.GetRequiredService<IAuthData>();
	}

	public HttpRequestMessage BuildRequest( HttpRequestMessage request )
	{
		
		// todo: implement
		throw new NotImplementedException();
	}
}