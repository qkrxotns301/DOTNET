using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiGateway.Infrastructure
{
    public sealed class RoutingManager
    {
        private readonly List<RedirectionRules> _redirectionRules;
        private RoutingManager()
        {
            _redirectionRules = JsonLoader.LoadFromFile<List<RedirectionRules>>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "routes.json"));
        }

        private static readonly Lazy<RoutingManager>
            lazy =
            new Lazy<RoutingManager>
                (() => new RoutingManager());

        public static RoutingManager Instance { get { return lazy.Value; } }
        public string GetRoute(string requestUrl)
        {
            var urlParts = requestUrl.Split('/');
            if (urlParts == null)
                return _redirectionRules.FirstOrDefault(x => x.IsDefault).DestinationUrl;

            var destinationEndPoint = _redirectionRules.FirstOrDefault(x => !x.IsDefault && x.EndPoint.ToLowerInvariant().Contains(urlParts[0].ToLowerInvariant()));
            if(destinationEndPoint == null)
                return _redirectionRules.FirstOrDefault(x => x.IsDefault).DestinationUrl;
            return destinationEndPoint.DestinationUrl;
        }
    }
}