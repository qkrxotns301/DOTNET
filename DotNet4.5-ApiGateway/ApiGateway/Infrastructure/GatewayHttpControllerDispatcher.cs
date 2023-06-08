using ApiGateway.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway
{
    public class GatewayHttpControllerDispatcher : HttpMessageHandler
    {
        private readonly DestinationDispatcher _dispatcher;
        private readonly RoutingManager _routingManager;

        public GatewayHttpControllerDispatcher()
        {
            _dispatcher = DestinationDispatcher.Instance;
            _routingManager = RoutingManager.Instance;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var destinationUrl = _routingManager.GetRoute(request.RequestUri.AbsolutePath);
                return await _dispatcher.SendRequest(request, destinationUrl);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex, "Unable to route request");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}