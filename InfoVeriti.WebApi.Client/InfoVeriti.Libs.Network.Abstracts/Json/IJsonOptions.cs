using System.Text.Json;

namespace InfoVeriti.Libs.Network.Abstracts.Json;

public interface IJsonOptions
{
	JsonSerializerOptions SerializerOptions { get; }
}