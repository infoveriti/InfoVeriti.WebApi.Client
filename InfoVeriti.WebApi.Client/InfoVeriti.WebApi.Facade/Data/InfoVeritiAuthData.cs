using InfoVeriti.Libs.Network.Abstracts.Data;
using InfoVeriti.WebApi.Contracts.Auth;
using InfoVeriti.WebApi.Facade.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace InfoVeriti.WebApi.Facade.Data;

public class InfoVeritiAuthData: IAuthData
{
	private readonly IServiceProvider _serviceProvider;

	public string? Url => GetConfig().Url;
	
	public string? Token => GetAuthResponse().Token;
	
	public string? ClientHash => GetAuthResponse().ClientHash;
	
	public DateTimeOffset? Expire => GetAuthResponse().ExpiresAt;
	
	public string? DeviceId => GetConfig().DeviceId; 
	public string? DeviceKey => GetConfig().Key;
	public string? DeviceName => GetConfig().DeviceName;
	
	public bool IsValid => GetAuthResponse().IsValid && GetConfig().IsValid;

	public InfoVeritiAuthData(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}
	
	
	private AuthResponse GetAuthResponse()
	{
		var store = _serviceProvider.GetRequiredService<IAuthStore>();
		return store.Load( default ) ?? new AuthResponse();
	}
	
	private Config GetConfig()
	{
		return _serviceProvider.GetRequiredService<Config>();
	}

}