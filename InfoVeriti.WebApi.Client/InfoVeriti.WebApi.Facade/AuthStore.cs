using System.Text;
using System.Text.Json;
using InfoVeriti.WebApi.Contracts.Auth;
using InfoVeriti.WebApi.Facade.Abstracts;

namespace InfoVeriti.WebApi.Facade;

public class AuthStore: IAuthStore
{
	private AuthResponse? _last = null;

	public AuthResponse? Load( CancellationToken token = default )
	{
		if (_last is not null)
			return _last;
		
		token.ThrowIfCancellationRequested();
		
		var appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
		var filePath = Path.Combine( appDataPath, "infoveriti-webapi-auth.json" );
		if ( !File.Exists( filePath ) )
			return null;
		
		_last = JsonSerializer.Deserialize<AuthResponse>( File.ReadAllText( filePath ) );
		return _last;
	}

	public void Save( AuthResponse authResponse, CancellationToken token = default )
	{
		_last = authResponse;
		
		var appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
		var filePath = Path.Combine( appDataPath, "infoveriti-webapi-auth.json" );
		var fi = new FileInfo( filePath );
		fi.Refresh();
		if ( fi.Exists )
			fi.Delete();
		
		File.WriteAllText( filePath, JsonSerializer.Serialize( authResponse ), new UTF8Encoding(false) );
	}
}