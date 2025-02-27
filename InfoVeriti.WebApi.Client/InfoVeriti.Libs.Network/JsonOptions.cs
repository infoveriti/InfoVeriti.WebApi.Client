using System.Text.Json;
using InfoVeriti.Libs.Network.Abstracts.Json;

namespace InfoVeriti.Libs.Network;

public sealed class JsonOptions: IJsonOptions
{
	public JsonOptions( JsonSerializerOptions serializerOptions )
	{
		SerializerOptions = serializerOptions;
	}

	public JsonSerializerOptions SerializerOptions { get; }
}