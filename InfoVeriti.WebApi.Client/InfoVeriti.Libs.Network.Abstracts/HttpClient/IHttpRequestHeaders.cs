namespace InfoVeriti.Libs.Network.Abstracts.HttpClient;

public interface IHttpRequestHeaders: IEnumerable<KeyValuePair<string, IEnumerable<string>>>
{
	public void Add(string name, string? value);
	
	public void Add(string name, IEnumerable<string?> values);
	
	public bool Remove(string name);

	public void Clear();
	
	public IEnumerable<string> GetValues(string name);
}