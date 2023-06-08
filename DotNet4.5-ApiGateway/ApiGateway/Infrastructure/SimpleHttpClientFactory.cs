using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ApiGateway.Infrastructure
{
    public sealed class SimpleHttpClientFactory
    {
        private static readonly Lazy<SimpleHttpClientFactory>
            lazy =
            new Lazy<SimpleHttpClientFactory>
                (() => new SimpleHttpClientFactory());

        public static SimpleHttpClientFactory Instance { get { return lazy.Value; } }

        private readonly Dictionary<string, HttpClient> _httpClientFactory = new Dictionary<string, HttpClient>();
        private SimpleHttpClientFactory()
        {
        }

        public HttpClient GetHttpClient(string url)
        {
            if(!_httpClientFactory.ContainsKey(url.ToLowerInvariant()))
            {
                _httpClientFactory[url.ToLowerInvariant()] = new HttpClient();
            }

            return _httpClientFactory[url.ToLowerInvariant()];
        }
    }
}