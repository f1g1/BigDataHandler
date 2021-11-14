using AutoMapper;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using BigDataHandler.Dtos;
using BigDataHandler.EFConfigs;
using BigDataHandler.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigDataHandler.EventHubReader
{
    public class FromPhoneReader : IHostedService
    {
        private IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        static BlobContainerClient _storageClient;
        static EventProcessorClient _processor;
        private ConcurrentDictionary<string, int> partitionEventCount = new();
        private readonly string _eventHubName;

        public FromPhoneReader(IConfiguration configuration, IServiceProvider serviceProvider, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            var ehubNamespaceConnectionString = _configuration.GetConnectionString("EventHub");
            _eventHubName = _configuration.GetValue<string>("FromPhoneEH");
            var blobStorageConnectionString = _configuration.GetConnectionString("BlobStorage");
            var blobContainerName = _configuration.GetValue<string>("BlobContainerName");
            var consumerGroup = _configuration.GetValue<string>("EHConsumerGroup");

            _storageClient = new BlobContainerClient(
                blobStorageConnectionString,
                blobContainerName);

            _processor = new EventProcessorClient(
               _storageClient,
               consumerGroup,
               ehubNamespaceConnectionString,
               _eventHubName);
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {

            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;

            await _processor.StartProcessingAsync();
        }

        private Task ProcessErrorHandler(ProcessErrorEventArgs args)
        {
            try
            {
                Debug.WriteLine("Error in the EventProcessorClient");
                Debug.WriteLine($"\tOperation: { args.Operation }");
                Debug.WriteLine($"\tException: { args.Exception }");
                Debug.WriteLine("");
            }
            catch
            {

            }

            return Task.CompletedTask;

        }

        private async Task ProcessEventHandler(ProcessEventArgs args)
        {
            try
            {
                if (args.CancellationToken.IsCancellationRequested)
                {
                    return;
                }

                string partition = args.Partition.PartitionId;
                //var eventBody = (DataStamp(args.Data.EventBody.ToArray());
                var jsonBody = Encoding.UTF8.GetString(args.Data.EventBody.ToArray());
                var dataStamp = JsonConvert.DeserializeObject<DtoDataStamp>(jsonBody);
                dataStamp.Source = _eventHubName;
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext =
                        scope.ServiceProvider.GetRequiredService<BigDataContext>();

                    var ds = _mapper.Map<DataStamp>(dataStamp);
                    _bigDataContext.Add(ds);
                    _bigDataContext.SaveChanges();
                    Debug.WriteLine($"Event from partition { partition } object:{ JsonConvert.SerializeObject(jsonBody)}.");

                    var currentEntities = _bigDataContext.DataStamps.Where((x) => true).ToList();
                }

                int eventsSinceLastCheckpoint = partitionEventCount.AddOrUpdate(
                    key: partition,
                    addValue: 1,
                    updateValueFactory: (_, currentCount) => currentCount + 1);

                if (eventsSinceLastCheckpoint >= 50)
                {
                    await args.UpdateCheckpointAsync();
                    partitionEventCount[partition] = 0;
                }
            }
            catch
            {
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

            _processor.ProcessEventAsync -= ProcessEventHandler;
            _processor.ProcessErrorAsync -= ProcessErrorHandler;

            await _processor.StopProcessingAsync();
        }
    }
}
