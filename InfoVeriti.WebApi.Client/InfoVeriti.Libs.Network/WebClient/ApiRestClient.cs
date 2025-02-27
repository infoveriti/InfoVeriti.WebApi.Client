using InfoVeriti.Libs.Network.Abstracts.HttpClient;
using InfoVeriti.Libs.Network.Abstracts.Json;
using InfoVeriti.Libs.Network.Abstracts.WebApi;
using InfoVeriti.Libs.Network.Abstracts.WebClient;

namespace InfoVeriti.Libs.Network.WebClient
{
    public class ApiRestClient: RestWebClientReactive, IApiClient
    {
        private Uri _apiUrl;

        public Uri ApiUrl
        {
            get => _apiUrl;
            set => _apiUrl = value;
        }

        public ApiRestClient( Uri apiUrl, IJsonOptions jsonOptions, IHttpClient httpClient ) : base( jsonOptions, httpClient, new TransparentRequestBuilder(), new TransparentInterceptor() )
        {
            _apiUrl = apiUrl ?? throw new ArgumentNullException( nameof( apiUrl ) );
        }

        public ApiRestClient( string apiUrl, IJsonOptions jsonOptions, IHttpClient httpClient ) : this( new Uri( apiUrl ), jsonOptions, httpClient )
        {
        }

        public override IWebClientReactive WithUrl( string relativeUrl )
        {
            var url = ApiUrl.ToString().TrimEnd('/') + relativeUrl;
            return base.WithUrl( url );
        }
    }
}