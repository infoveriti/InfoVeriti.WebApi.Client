using System.Net;

namespace InfoVeriti.Libs.Network.Abstracts.HttpClient
{
    public interface IHttpClient
    {
        public static IWebProxy DefaultProxy { get => System.Net.Http.HttpClient.DefaultProxy; set => System.Net.Http.HttpClient.DefaultProxy = value; }
        

        #region Properties

        IHttpRequestHeaders DefaultRequestHeaders { get; }
        

        Version DefaultRequestVersion { get; set; }

        Uri? BaseAddress { get; set; }

        TimeSpan Timeout { get; set; }

        long MaxResponseContentBufferSize { get; set; }

        #endregion Properties


        #region Public Send

        #region Simple Get Overloads

        Task<string> GetStringAsync( string requestUri );

        Task<string> GetStringAsync( Uri requestUri );

        Task<byte[]> GetByteArrayAsync( string requestUri );

        Task<byte[]> GetByteArrayAsync( Uri requestUri );


        // Unbuffered by default
        Task<Stream> GetStreamAsync( string requestUri );

        // Unbuffered by default
        Task<Stream> GetStreamAsync( Uri requestUri );

        #endregion Simple Get Overloads

        #region REST Send Overloads

        Task<HttpResponseMessage> GetAsync( string requestUri );

        Task<HttpResponseMessage> GetAsync( Uri requestUri );

        Task<HttpResponseMessage> GetAsync( string requestUri, HttpCompletionOption completionOption );

        Task<HttpResponseMessage> GetAsync( Uri requestUri, HttpCompletionOption completionOption );

        Task<HttpResponseMessage> GetAsync( string requestUri, CancellationToken cancellationToken );

        Task<HttpResponseMessage> GetAsync( Uri requestUri, CancellationToken cancellationToken );

        Task<HttpResponseMessage> GetAsync( string requestUri, HttpCompletionOption completionOption,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> GetAsync( Uri requestUri, HttpCompletionOption completionOption,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> PostAsync( string requestUri, HttpContent content );

        Task<HttpResponseMessage> PostAsync( Uri requestUri, HttpContent content );

        Task<HttpResponseMessage> PostAsync( string requestUri, HttpContent content,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> PostAsync( Uri requestUri, HttpContent content,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> PutAsync( string requestUri, HttpContent content );

        Task<HttpResponseMessage> PutAsync( Uri requestUri, HttpContent content );

        Task<HttpResponseMessage> PutAsync( string requestUri, HttpContent content,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> PutAsync( Uri requestUri, HttpContent content,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> PatchAsync( string requestUri, HttpContent content );

        Task<HttpResponseMessage> PatchAsync( Uri requestUri, HttpContent content );

        Task<HttpResponseMessage> PatchAsync( string requestUri, HttpContent content,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> PatchAsync( Uri requestUri, HttpContent content,
            CancellationToken cancellationToken );

        Task<HttpResponseMessage> DeleteAsync( string requestUri );

        Task<HttpResponseMessage> DeleteAsync( Uri requestUri );

        Task<HttpResponseMessage> DeleteAsync( string requestUri, CancellationToken cancellationToken );

        Task<HttpResponseMessage> DeleteAsync( Uri requestUri, CancellationToken cancellationToken );

        #endregion REST Send Overloads

        #region Advanced Send Overloads

        Task<HttpResponseMessage> SendAsync( HttpRequestMessage request );

        Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken );

        Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, HttpCompletionOption completionOption );

        Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, HttpCompletionOption completionOption,
            CancellationToken cancellationToken );


        public void CancelPendingRequests();

        #endregion Advanced Send Overloads

        #endregion Public Send


        
        
    }
}