using System.Net;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json;
using InfoVeriti.Libs.Network.Abstracts.HttpClient;
using InfoVeriti.Libs.Network.Abstracts.Json;
using InfoVeriti.Libs.Network.Abstracts.WebClient;
using InfoVeriti.Libs.Network.Exceptions;

namespace InfoVeriti.Libs.Network.WebClient
{
    public class RestWebClientReactive : IWebClientReactive
    {
        private readonly IRequestBuilder _requestBuilder;


        internal IWebClientInterceptor Interceptor { get; }

        internal IHttpClient HttpClient { get; }

        internal IJsonOptions JsonOptions { get; }

        public Uri? Url { get; private set; }
        public object? Body { get; private set; }

        public virtual IWebClientReactive WithUrl( string url )
        {
            if ( string.IsNullOrWhiteSpace( url ) == false )
                return WithUrl( new Uri( url ) );

            throw new ArgumentNullException( nameof( url ) );
        }

        public virtual IWebClientReactive WithUrl( Uri uri )
        {
            Url = uri;
            return this;
        }

        public virtual IWebClientReactive WithBody<T>( T body ) where T : class
        {
            Body = body;
            return this;
        }

        public virtual IWebClientReactive WithoutBody()
        {
            Body = null;
            return this;
        }


        public RestWebClientReactive(IJsonOptions jsonOptions, IHttpClient httpClient, IRequestBuilder requestBuilder, IWebClientInterceptor interceptor  )
        {
            _requestBuilder = requestBuilder ?? throw new ArgumentNullException( nameof( requestBuilder ) );
            JsonOptions = jsonOptions ?? throw new ArgumentNullException( nameof( jsonOptions ) );
            HttpClient = httpClient ?? throw new ArgumentNullException( nameof( httpClient ) );
            Interceptor = interceptor ?? throw new ArgumentNullException( nameof( interceptor ) );
        }


        public virtual IObservable<T> Get<T>() where T : class, new()
        {
            CheckUrl();
            try
            {
                return Observable.Return( GetResponse<T>( HttpMethod.Get ) );
            }
            catch ( Exception ex )
            {
                return Observable.Throw<T>( ex );
            }
        }

        public virtual IObservable<T> GetAsync<T>() where T : class, new()
        {
            CheckUrl();
            return Observable.Start( () => GetResponse<T>( HttpMethod.Get ) );
        }

        public virtual IObservable<T> Post<T>() where T : class, new()
        {
            try
            {
                return Observable.Return( GetResponse<T>( HttpMethod.Post ) );
            }
            catch ( Exception ex )
            {
                return Observable.Throw<T>( ex );
            }
        }

        public virtual IObservable<T> PostAsync<T>() where T : class, new()
        {
            CheckUrl();
            return Observable.Start( () => GetResponse<T>( HttpMethod.Post ) );
        }

        public IObservable<TResult> Put<TResult>() where TResult : class, new()
        {
            CheckUrl();
            try
            {
                return Observable.Return( GetResponse<TResult>( HttpMethod.Put ) );
            }
            catch ( Exception ex )
            {
                return Observable.Throw<TResult>( ex );
            }

        }

        public IObservable<TResult> PutAsync<TResult>() where TResult : class, new()
        {
            CheckUrl();
            return Observable.Start( () => GetResponse<TResult>( HttpMethod.Put ) );
        }

        public IObservable<TResult> Delete<TResult>() where TResult : class, new()
        {
            CheckUrl();
            try
            {
                return Observable.Return( GetResponse<TResult>( HttpMethod.Delete ) );
            }
            catch ( Exception ex )
            {
                return Observable.Throw<TResult>( ex );
            }
        }

        public IObservable<TResult> DeleteAsync<TResult>() where TResult : class, new()
        {
            CheckUrl();
            return Observable.Start( () => GetResponse<TResult>( HttpMethod.Delete ) );
        }





        #region help funcs

        private void CheckUrl()
        {
            if ( Url is null )
                throw new Exception( String.Format("{0}.Url is empty (use WithUrl method)", GetType().Name ) );
        }
        
        private T GetResponse<T>( HttpMethod method ) where T : class, new()
        {
            Exception? ex = null;
            var result = new T();
            string? bodyText = null;
            var request = _requestBuilder.BuildRequest(new HttpRequestMessage( method, Url ));

            if ( Body != null )
            {
                var isString = (Body is string);
                var body = (isString ? (string) Body : JsonSerializer.Serialize( Body, JsonOptions.SerializerOptions ));
                request.Content = new StringContent( body, Encoding.UTF8, (isString ? "text/plain": "application/json") );
            }

            
            var taskGet = HttpClient.SendAsync( request );
            taskGet.Wait();

            var response = taskGet.Result;

            var statusCode = HttpStatusCode.Conflict; 
            try
            {
                statusCode = response.StatusCode;
                response = Interceptor.OnIntercept( WebClientOnInterceptParameters.CreateInstance( HttpClient, request, response ) );
                statusCode = response.StatusCode;
                
                
                response.Content.ReadAsStringAsync().ContinueWith( ( taskContent ) =>
                {
                    bodyText = taskContent.Result;
                    if ( !string.IsNullOrWhiteSpace( bodyText ) )
                    {
                        try
                        {
                            result = JsonSerializer.Deserialize<T>( bodyText, new JsonSerializerOptions() { PropertyNamingPolicy = NoChangeJsonNamingPolicy.Instance, DictionaryKeyPolicy = NoChangeJsonNamingPolicy.Instance } );

                        }
                        catch ( Exception e )
                        {
                            ex = e;
                        }

                    }
                } ).Wait();
                
                if (ex is null)
                    response.EnsureSuccessStatusCode();
                
            }
            catch ( Exception e )
            {
                ex = new HttpStatusCodeException( statusCode, request, result, bodyText, e );
            }

            if ( ex != null )
                throw ex;

            return result;
        }

        #endregion


    }
    
    internal class NoChangeJsonNamingPolicy: JsonNamingPolicy
    {
        public static NoChangeJsonNamingPolicy Build()
        {
            return Instance;
        }

        private static readonly Lazy<NoChangeJsonNamingPolicy> LazyInstance = new(() => new NoChangeJsonNamingPolicy(), true);

        public static NoChangeJsonNamingPolicy Instance => LazyInstance.Value;

        private NoChangeJsonNamingPolicy()
        {
        }

        public override string ConvertName( string name )
        {
            return name;
        }
    }

}