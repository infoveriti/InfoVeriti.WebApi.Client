namespace InfoVeriti.WebApi.Contracts.Others;

public class TimeSyncResponse: ErrorApiResponse
{
	public decimal Timestamp { get; set; } = 0.0m;
}