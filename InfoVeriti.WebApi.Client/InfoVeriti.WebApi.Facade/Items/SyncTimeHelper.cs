using System.Globalization;
using InfoVeriti.Libs.Network.Abstracts.WebApi;
using InfoVeriti.WebApi.Contracts.Others;
using InfoVeriti.WebApi.Core.Values.Time;
using InfoVeriti.WebApi.Facade.Extensions;

namespace InfoVeriti.WebApi.Facade.Items;

internal class SyncTimeHelper
{
	private readonly IApiClient _apiClient;

	private SyncTimeHelper(IApiClient apiClient)
	{
		_apiClient = apiClient;
	}

	internal static SyncTimeHelper CreateInstance( IApiClient apiClient )
	{
		return new SyncTimeHelper( apiClient );
	}

	internal decimal GetApiTimeDifference()
	{
		var startTime = Timestamp.Now.UnixTimeMilliseconds / 1000.0m;
		var wait = true;
		decimal timeDifference = decimal.MaxValue;
		Exception? ex = null;
		_apiClient.WithUrl( "/Timestamp" ).Get<TimeSyncResponse>().Subscribe( ( next ) =>
			{
				if (next.IsError)
				{
					ex = new Exception( next.Error );
					wait = false;
					return;
				}
				
				var endTime = Timestamp.Now.UnixTimeMilliseconds / 1000.0m;
				var serverTime = next.Timestamp;
				var duration = endTime - startTime;
				timeDifference = startTime + duration - serverTime;
				Console.WriteLine( "Time difference: {0}, duration: {1}", timeDifference.ToString(CultureInfo.InvariantCulture), duration.ToString(CultureInfo.InvariantCulture) );
				wait = false;

			}, ( exception ) =>
			{
				ex = new Exception( exception.GetHttpStatusMessage(), exception);
				wait = false;
			},
			() =>
			{
				wait = false;
			}
		);

		while (wait)
			Task.Delay( 100 ).Wait();
		
		if (ex != null)
			throw ex;


		return timeDifference;
	}

}