using System.Collections;
using System.Net.Http.Headers;
using InfoVeriti.Libs.Network.Abstracts.HttpClient;

namespace InfoVeriti.Libs.Network.HttpClient;

public class HttpRequestHeadersWrapper: IHttpRequestHeaders
{
	private readonly HttpRequestHeaders _headers;

	private HttpRequestHeadersWrapper( HttpRequestHeaders headers )
	{
		_headers = headers;
	}

	public static HttpRequestHeadersWrapper CreateInstance( HttpRequestHeaders? headers )
	{
		if (headers is null)
			throw new ArgumentNullException( nameof( headers ) );
		
		return new HttpRequestHeadersWrapper( headers );
	}

	public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator()
	{
		return _headers.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Add( string name, string? value )
	{
		_headers.Add( name, value );
	}

	public void Add( string name, IEnumerable<string?> values )
	{
		_headers.Add( name, values );
	}

	public bool Remove( string name )
	{
		return _headers.Remove( name );
	}

	public void Clear()
	{
		_headers.Clear();
	}

	public IEnumerable<string> GetValues( string name )
	{
		return _headers.GetValues( name );
	}
}