using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfoVeriti.WebApi.Client.Extensions;

internal static class ObjectExtensions
{
	public static string ToJson( this object obj, JsonSerializerOptions? options = null ) => JsonSerializer.Serialize( obj, options ?? new JsonSerializerOptions() { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull } );
}