using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Diagnostics;
using System.Collections.Concurrent;
using BigDataHandler.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BigDataHandler.EventHubReader
{
    public class FromPhoneReader : IHostedService
    {
        private IConfiguration _configuration;
        static BlobContainerClient _storageClient;
        static EventProcessorClient _processor;
        private ConcurrentDictionary<string, int> partitionEventCount = new();

        public FromPhoneReader(IConfiguration configuration)
        {
            _configuration = configuration;

            var ehubNamespaceConnectionString = _configuration.GetConnectionString("EventHub");
            var eventHubName = _configuration.GetValue<string>("FromPhoneEH");
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
               eventHubName);
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {

            // Register handlers for processing events and handling errors
            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
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
                var myPoco = JsonConvert.DeserializeObject<DataStamp>(jsonBody);
                Debug.WriteLine($"Event from partition { partition } object:{ JsonConvert.SerializeObject(jsonBody)}.");

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
                // It is very important that you always guard against
                // exceptions in your handler code; the processor does
                // not have enough understanding of your code to
                // determine the correct action to take.  Any
                // exceptions from your handlers go uncaught by
                // the processor and will NOT be redirected to
                // the error handler.
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the processing
            await _processor.StopProcessingAsync();
        }

        //public object ByteArrayToObject(byte[] arrBytes)
        //{
        //    using (var memStream = new MemoryStream())
        //    {
        //        var binForm = new BinaryFormatter();
        //        memStream.Write(arrBytes, 0, arrBytes.Length);
        //        memStream.Seek(0, SeekOrigin.Begin);
        //        object obj = binForm.Deserialize(memStream);
        //        return obj;
        //    }
        //}
    }
}
