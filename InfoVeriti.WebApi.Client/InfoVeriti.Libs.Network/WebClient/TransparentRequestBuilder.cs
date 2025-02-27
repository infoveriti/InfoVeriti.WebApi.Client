using InfoVeriti.Libs.Network.Abstracts.WebClient;

namespace InfoVeriti.Libs.Network.WebClient;

public class TransparentRequestBuilder: IRequestBuilder
{
	public HttpRequestMessage BuildRequest( HttpRequestMessage request )
	{
		return request;
	}
}