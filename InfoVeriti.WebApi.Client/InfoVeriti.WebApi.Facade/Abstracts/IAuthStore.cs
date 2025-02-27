using InfoVeriti.WebApi.Contracts.Auth;

namespace InfoVeriti.WebApi.Facade.Abstracts;

public interface IAuthStore
{
		
		
	AuthResponse? Load( CancellationToken token = default);
	
	void Save( AuthResponse authResponse, CancellationToken token = default);
}