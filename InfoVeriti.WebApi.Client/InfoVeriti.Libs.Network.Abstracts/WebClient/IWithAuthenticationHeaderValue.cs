namespace InfoVeriti.Libs.Network.Abstracts.WebClient
{
    public interface IWithAuthenticationHeaderValue
    {
        void AddAuthenticationHeaderValue( string schema, string value );
    }
}