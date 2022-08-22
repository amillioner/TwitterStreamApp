using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Twitter.StreamApp.Stream.Stream.Interfaces;

namespace Twitter.StreamApp.Stream.Stream
{
    public class StreamServiceManager : IStreamServiceManager
    {
        private readonly ILogger<StreamServiceManager> _logger;
        private readonly IDictionary<Type, IStreamService> _map = new Dictionary<Type, IStreamService>();

        public StreamServiceManager(
            ILogger<StreamServiceManager> logger
            , IEnumerable<IStreamService> streamServices)
        {
            _logger = logger;
            Set(streamServices);
        }

        private void Set(IEnumerable<IStreamService> streamServices)
        {
            var services = streamServices.ToList();
            foreach (var service in services)
                _map[service.GetType()] = service;
        }

        public IStreamService Get(Type service)
        {
            if (_map.ContainsKey(service))
                return _map[service];

            var msg = "Unable to find the service";
            _logger.LogError(msg);
            throw new Exception(msg);
        }
    }
}