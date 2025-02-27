using System.Text.Json;
using System.Text.Json.Serialization;
using InfoVeriti.Libs.Network;
using InfoVeriti.Libs.Network.Abstracts.Data;
using InfoVeriti.Libs.Network.Abstracts.HttpClient;
using InfoVeriti.Libs.Network.Abstracts.Json;
using InfoVeriti.Libs.Network.Abstracts.WebApi;
using InfoVeriti.Libs.Network.Abstracts.WebClient;
using InfoVeriti.Libs.Network.HttpClient;
using InfoVeriti.Libs.Network.WebClient;
using InfoVeriti.WebApi.Core.Values.Time;
using InfoVeriti.WebApi.Facade;
using InfoVeriti.WebApi.Facade.Abstracts;
using InfoVeriti.WebApi.Facade.Data;
using Microsoft.Extensions.DependencyInjection;

namespace InfoVeriti.WebApi.Client.Extensions;

internal static class ServicesExtensions
{
	public static IServiceCollection AddConfig( this IServiceCollection services )
	{
		return services
				.AddSingleton<Config>( _ => Config.Load( "config.json" ) )
				.AddSingleton<IJsonOptions>(_ => new JsonOptions( new JsonSerializerOptions() { WriteIndented = false, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }) )
				.AddSingleton<IAuthData>( (s) => new InfoVeritiAuthData( s ) )
			;
	}
	
	public static IServiceCollection AddHelpers(this IServiceCollection services)
	{
		return services
			.AddSingleton<IAuthStore>((s) => new AuthStore())			
			.AddTransient<IHttpClient>((s) => new HttpClientWrapper( new HttpClient() ))
			.AddSingleton<IApiClient>((s) => new ApiRestClient( s.GetRequiredService<Config>().Url ?? throw new ArgumentNullException($"{nameof(Config)}.{nameof(Config.Url)}"), s.GetRequiredService<IJsonOptions>(),  s.GetRequiredService<IHttpClient>() ))
			;
	}
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		return services;
	}
	
	public static IServiceCollection Init(this IServiceCollection services)
	{
		var now = DateTimeOffset.Now;
		Time.DefaultOffset = now.Offset;
		
		return services
				.AddConfig()
			.AddHelpers()
			.AddServices()
			;
	}
	
	
}