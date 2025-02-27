using InfoVeriti.Libs.Network.Abstracts.HttpClient;
using InfoVeriti.Libs.Network.Abstracts.Json;

namespace InfoVeriti.Libs.Network.Abstracts.WebClient
{
    public interface IWebClientReactive
    {
        
        Uri? Url { get; }
        
        object? Body { get; }
        
        IWebClientReactive WithUrl(string url);
        IWebClientReactive WithUrl(Uri uri);

        IWebClientReactive WithBody<T>( T body )  where T : class;

        IWebClientReactive WithoutBody();

        IObservable<TResult> Get<TResult>() where TResult : class, new();

        IObservable<TResult> GetAsync<TResult>() where TResult : class, new();

        IObservable<TResult> Post<TResult>() where TResult : class, new();

        IObservable<TResult> PostAsync<TResult>() where TResult : class, new();

        IObservable<TResult> Put<TResult>() where TResult : class, new();

        IObservable<TResult> PutAsync<TResult>() where TResult : class, new();

        IObservable<TResult> Delete<TResult>() where TResult : class, new();

        IObservable<TResult> DeleteAsync<TResult>() where TResult : class, new();
    }
}