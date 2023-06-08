using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Infrastructure
{
    public sealed class DestinationDispatcher
    {
        private static readonly Lazy<DestinationDispatcher>
           lazy =
           new Lazy<DestinationDispatcher>
               (() => new DestinationDispatcher());

        public static DestinationDispatcher Instance { get { return lazy.Value; } }
        public string Path { get; set; }
        private DestinationDispatcher()
        {
        }

        public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, string uri = "")
        {
            try
            {
                Path = string.IsNullOrEmpty(uri) ? "/" : uri;
                var client = SimpleHttpClientFactory.Instance.GetHttpClient(Path);

                using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method.Method), Path))
                {
                    newRequest.Content = await request.Content.ReadAsAsync<HttpContent>();
                    using (var response = await client.SendAsync(newRequest))
                    {
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex, "Unable to route request");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
           
        }
    }
}