using System.Net.Http.Headers;
using InfoVeriti.Libs.Network.Abstracts.HttpClient;

namespace InfoVeriti.Libs.Network.HttpClient
{
    public class HttpClientWrapper: IHttpClient
    {
        private IHttpRequestHeaders? _defaultRequestHeaders = null;
        public System.Net.Http.HttpClient HttpClientSystem { get; private set; }

        public IHttpRequestHeaders DefaultRequestHeaders => GetDefaultRequestHeaders();

        private IHttpRequestHeaders GetDefaultRequestHeaders()
        {
            if (_defaultRequestHeaders is null)
                _defaultRequestHeaders = HttpRequestHeadersWrapper.CreateInstance( HttpClientSystem.DefaultRequestHeaders );

            return _defaultRequestHeaders;
        }

        public Version DefaultRequestVersion { get => HttpClientSystem.DefaultRequestVersion; set => HttpClientSystem.DefaultRequestVersion = value; }
        public Uri? BaseAddress { get => HttpClientSystem.BaseAddress; set => HttpClientSystem.BaseAddress = value; }
        public TimeSpan Timeout { get => HttpClientSystem.Timeout; set => HttpClientSystem.Timeout = value; }
        public long MaxResponseContentBufferSize { get => HttpClientSystem.MaxResponseContentBufferSize; set => HttpClientSystem.MaxResponseContentBufferSize = value; }

        public HttpClientWrapper( System.Net.Http.HttpClient systemHttpClient )
        {
            HttpClientSystem = systemHttpClient ?? throw new ArgumentNullException( nameof(systemHttpClient)  );
            HttpClientSystem.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json") );
        }

        public Task<string> GetStringAsync( string requestUri )
        {
            return HttpClientSystem.GetStringAsync( requestUri );
        }

        public Task<string> GetStringAsync( Uri requestUri )
        {
            return HttpClientSystem.GetStringAsync( requestUri );
        }

        public Task<byte[]> GetByteArrayAsync( string requestUri )
        {
            return HttpClientSystem.GetByteArrayAsync( requestUri );
        }

        public Task<byte[]> GetByteArrayAsync( Uri requestUri )
        {
            return HttpClientSystem.GetByteArrayAsync( requestUri );
        }

        public Task<Stream> GetStreamAsync( string requestUri )
        {
            return HttpClientSystem.GetStreamAsync( requestUri );
        }

        public Task<Stream> GetStreamAsync( Uri requestUri )
        {
            return HttpClientSystem.GetStreamAsync( requestUri );
        }

        public Task<HttpResponseMessage> GetAsync( string requestUri )
        {
            return HttpClientSystem.GetAsync( requestUri );
        }

        public Task<HttpResponseMessage> GetAsync( Uri requestUri )
        {
            return HttpClientSystem.GetAsync( requestUri );
        }

        public Task<HttpResponseMessage> GetAsync( string requestUri, HttpCompletionOption completionOption )
        {
            return HttpClientSystem.GetAsync( requestUri, completionOption );
        }

        public Task<HttpResponseMessage> GetAsync( Uri requestUri, HttpCompletionOption completionOption )
        {
            return HttpClientSystem.GetAsync( requestUri, completionOption );
        }

        public Task<HttpResponseMessage> GetAsync( string requestUri, CancellationToken cancellationToken )
        {
            return HttpClientSystem.GetAsync( requestUri, cancellationToken );
        }

        public Task<HttpResponseMessage> GetAsync( Uri requestUri, CancellationToken cancellationToken )
        {
            return HttpClientSystem.GetAsync( requestUri, cancellationToken );
        }

        public Task<HttpResponseMessage> GetAsync( string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken )
        {
            return HttpClientSystem.GetAsync( requestUri, completionOption, cancellationToken );
        }

        public Task<HttpResponseMessage> GetAsync( Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken )
        {
            return HttpClientSystem.GetAsync( requestUri, completionOption, cancellationToken );
        }

        public Task<HttpResponseMessage> PostAsync( string requestUri, HttpContent content )
        {
            return HttpClientSystem.PostAsync( requestUri, content );
        }

        public Task<HttpResponseMessage> PostAsync( Uri requestUri, HttpContent content )
        {
            return HttpClientSystem.PostAsync( requestUri, content );
        }

        public Task<HttpResponseMessage> PostAsync( string requestUri, HttpContent content, CancellationToken cancellationToken )
        {
            return HttpClientSystem.PostAsync( requestUri, content, cancellationToken );
        }

        public Task<HttpResponseMessage> PostAsync( Uri requestUri, HttpContent content, CancellationToken cancellationToken )
        {
            return HttpClientSystem.PostAsync( requestUri, content, cancellationToken );
        }

        public Task<HttpResponseMessage> PutAsync( string requestUri, HttpContent content )
        {
            return HttpClientSystem.PutAsync( requestUri, content );
        }

        public Task<HttpResponseMessage> PutAsync( Uri requestUri, HttpContent content )
        {
            return HttpClientSystem.PutAsync( requestUri, content );
        }

        public Task<HttpResponseMessage> PutAsync( string requestUri, HttpContent content, CancellationToken cancellationToken )
        {
            return HttpClientSystem.PutAsync( requestUri, content, cancellationToken );
        }

        public Task<HttpResponseMessage> PutAsync( Uri requestUri, HttpContent content, CancellationToken cancellationToken )
        {
            return HttpClientSystem.PutAsync( requestUri, content, cancellationToken );
        }

        public Task<HttpResponseMessage> PatchAsync( string requestUri, HttpContent content )
        {
            return HttpClientSystem.PatchAsync( requestUri, content );
        }

        public Task<HttpResponseMessage> PatchAsync( Uri requestUri, HttpContent content )
        {
            return HttpClientSystem.PatchAsync( requestUri, content );
        }

        public Task<HttpResponseMessage> PatchAsync( string requestUri, HttpContent content, CancellationToken cancellationToken )
        {
            return HttpClientSystem.PatchAsync( requestUri, content, cancellationToken );
        }

        public Task<HttpResponseMessage> PatchAsync( Uri requestUri, HttpContent content, CancellationToken cancellationToken )
        {
            return HttpClientSystem.PatchAsync( requestUri, content, cancellationToken );
        }

        public Task<HttpResponseMessage> DeleteAsync( string requestUri )
        {
            return HttpClientSystem.DeleteAsync( requestUri );
        }

        public Task<HttpResponseMessage> DeleteAsync( Uri requestUri )
        {
            return HttpClientSystem.DeleteAsync( requestUri );
        }

        public Task<HttpResponseMessage> DeleteAsync( string requestUri, CancellationToken cancellationToken )
        {
            return HttpClientSystem.DeleteAsync( requestUri, cancellationToken );
        }

        public Task<HttpResponseMessage> DeleteAsync( Uri requestUri, CancellationToken cancellationToken )
        {
            return HttpClientSystem.DeleteAsync( requestUri, cancellationToken );
        }

        public Task<HttpResponseMessage> SendAsync( HttpRequestMessage request )
        {
            return HttpClientSystem.SendAsync( request );
        }

        public Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            return HttpClientSystem.SendAsync( request, cancellationToken );
        }

        public Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, HttpCompletionOption completionOption )
        {
            return HttpClientSystem.SendAsync( request, completionOption );
        }

        public Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken )
        {
            return HttpClientSystem.SendAsync( request, completionOption, cancellationToken );
        }

        public void CancelPendingRequests()
        {
            HttpClientSystem.CancelPendingRequests();
        }
    }
}