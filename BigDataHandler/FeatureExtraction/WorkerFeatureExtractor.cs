using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System;

namespace BigDataHandler.FeatureExtraction
{
    public class WorkerFeatureExtractor : IHostedService
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        public WorkerFeatureExtractor(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
