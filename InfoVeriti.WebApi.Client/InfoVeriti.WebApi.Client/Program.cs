using InfoVeriti.Libs.Network.Abstracts.WebApi;
using InfoVeriti.WebApi.Client;
using InfoVeriti.WebApi.Client.Extensions;
using InfoVeriti.WebApi.Client.Helpers;
using InfoVeriti.WebApi.Contracts;
using InfoVeriti.WebApi.Facade;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var serviceProvider = services
				.Init()
				.BuildServiceProvider();

var webApi = InfoVeritiWebApiFacade.CreateInstance( serviceProvider );

try
{

	webApi.SyncTimeWithApi();

	webApi.Login( () =>
	{
		Console.WriteLine( "Enter login:" );
		return Console.ReadLine();
	}, () =>
	{
		Console.WriteLine( "Enter password:" );
		return ConsolePasswordReader.ReadPassword();
	} );
}
catch ( Exception ex )
{
	Console.WriteLine( "ERROR:" );
	Console.WriteLine( ex.Message );
	Console.WriteLine( "-----" );
	Console.WriteLine( ex.StackTrace );
}


Console.WriteLine( "END" );
