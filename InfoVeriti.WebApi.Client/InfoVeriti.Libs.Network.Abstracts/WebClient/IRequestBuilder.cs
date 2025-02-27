namespace InfoVeriti.Libs.Network.Abstracts.WebClient;

public interface IRequestBuilder
{
	HttpRequestMessage BuildRequest( HttpRequestMessage request );
}