namespace InfoVeriti.Libs.Network.Abstracts.Data;

public interface IAuthData
{
	string? Url { get; }
	
	string? Token { get; }
	
	string? ClientHash { get; }
	
	DateTimeOffset? Expire { get; }
	
	string? DeviceId { get; }
	
	string? DeviceKey { get; }
	
	string? DeviceName { get; }
	
	bool IsValid { get; }
}