namespace InfoVeriti.WebApi.Contracts.Auth;

public record AuthRequest( string UserName, string PasswordHash );
