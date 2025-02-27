using System.Globalization;
using InfoVeriti.Libs.Network.Abstracts.WebApi;
using InfoVeriti.WebApi.Contracts.Others;
using InfoVeriti.WebApi.Core.Values.Time;
using InfoVeriti.WebApi.Facade.Items;
using Microsoft.Extensions.DependencyInjection;



namespace InfoVeriti.WebApi.Facade;

public class InfoVeritiWebApiFacade
{
	private readonly IServiceProvider _serviceProvider;
	private decimal _timeDifference = decimal.Zero;
	
	
	private IApiClient ApiClient { get; }
	private Config Config { get; }
	
	
	private InfoVeritiWebApiFacade(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		Config = serviceProvider.GetRequiredService<Config>();
		ApiClient = serviceProvider.GetRequiredService<IApiClient>();
	}

	public static InfoVeritiWebApiFacade CreateInstance( IServiceProvider serviceProvider )
	{
		return new InfoVeritiWebApiFacade( serviceProvider );
	}


	public InfoVeritiWebApiFacade SyncTimeWithApi()
	{
		_timeDifference = SyncTimeHelper.CreateInstance( ApiClient ).GetApiTimeDifference();
		return this;
	}
	
	public InfoVeritiWebApiFacade Login( Func<string?> getLogin, Func<string?> getPassword )
	{
		var authResponse = LoginHelper.CreateInstance( ApiClient ).Login( getLogin, getPassword );
		return this;
	}

#region HELP FUNCS


#endregion
	
}