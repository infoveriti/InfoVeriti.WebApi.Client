using System.Text.Json.Serialization;

namespace InfoVeriti.WebApi.Contracts;

public class ErrorApiResponse
{
	public string? Error { get; set; }
	
	[JsonIgnore]
	public bool IsError => !string.IsNullOrWhiteSpace( Error );
}