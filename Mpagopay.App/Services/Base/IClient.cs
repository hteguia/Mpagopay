namespace Mpagopay.App.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}
