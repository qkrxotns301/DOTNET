﻿using Serilog;
using System.Configuration;

namespace ApiGateway.Infrastructure
{
    public static class Logger
    {
        private static ILogger _logger;

        public static ILogger Log
        {
            get
            {
                var template =
                    "{Timestamp:yyyy:MM:dd HH:mm:ss.fff} [{Level:4}] {Message}{NewLine}{Exception}";

                if (_logger == null)
                {
                    var logLocation = ConfigurationManager.AppSettings["LogLocation"];
                    _logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.RollingFile($"{logLocation}\\Apigateway-{{Date}}.log", outputTemplate: template)
                        .CreateLogger();
                }

                return _logger;
            }
        }
    }
}