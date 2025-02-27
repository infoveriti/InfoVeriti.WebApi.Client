using System.Text.Json.Serialization;

namespace InfoVeriti.WebApi.Contracts.Auth;

public class AuthResponse: ErrorApiResponse
{
	public string? Token { get; set; }

	public DateTimeOffset? ExpiresAt { get; set; }

	public string? ClientHash { get; set; }
	
	[JsonIgnore]
	public bool IsValid => !string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace( ClientHash ) && ExpiresAt.HasValue;
	
	
	[JsonIgnore]
	public bool IsInvalid => !IsValid;
}