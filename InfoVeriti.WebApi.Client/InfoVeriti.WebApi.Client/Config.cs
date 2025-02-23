using System.Text.Json;

namespace InfoVeriti.WebApi.Client;

internal record Config( string? DeviceId, string? Key, string? Url )
{
	
	internal static Config Load( string path )
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
	
}
