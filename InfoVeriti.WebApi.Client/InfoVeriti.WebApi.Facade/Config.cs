using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfoVeriti.WebApi.Facade;

public record Config( string? DeviceId, string? DeviceName, string? Key, string? Url )
{
	public static Config Load( string path )
	{
		var config = JsonSerializer.Deserialize<Config>( File.ReadAllText( path ) );
		
		if( config is null )
			throw new ArgumentNullException( nameof( config ) );
		
		if ( string.IsNullOrWhiteSpace(config.DeviceId) )
			throw new ArgumentException( $"No device identificator in config.json" );
		
		if (string.IsNullOrWhiteSpace( config.Key ))
			throw new ArgumentException( $"No key in config.json" );
		
		if (string.IsNullOrWhiteSpace( config.Url ))
			throw new ArgumentException( $"No url to InfoVeriti WebApi in config.json" );
		
		return config;
	}

	[JsonIgnore]
	public bool IsValid => !string.IsNullOrWhiteSpace( DeviceId ) && !string.IsNullOrWhiteSpace( DeviceName ) && !string.IsNullOrWhiteSpace( Key ) && !string.IsNullOrWhiteSpace( Url );
}
