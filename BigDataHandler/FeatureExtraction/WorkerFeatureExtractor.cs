using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System;
using System.Diagnostics;
using BigDataHandler.EFConfigs;
using System.Linq;
using BigDataHandler.Models;

namespace BigDataHandler.FeatureExtraction
{
    public class WorkerFeatureExtractor : IHostedService
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private Timer timer = null;
        private long startTimestamp = 0;
        private StatisticalFeatureExtractor featureExtractor;
        private TimeSpan queryInterval = TimeSpan.FromSeconds(10);

        public WorkerFeatureExtractor(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            featureExtractor = new StatisticalFeatureExtractor();

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                var dataStampsToBeProcessed = _bigDataContext.DataStamps.Where(x => x.IsProcessed == false).Count();
                if(dataStampsToBeProcessed == 0)
                {
                    // No data stamps to be processed present in the database, will begin to query every 10 seconds from the current time
                    startTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
                else
                {
                    // Start processing data from the oldest data stamp that is un processed
                    var oldestUnProcessedDataStamp = _bigDataContext.DataStamps.Where(x => !x.IsProcessed).OrderBy(x => x.Timestamp).First();
                    startTimestamp = oldestUnProcessedDataStamp.Timestamp;
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, queryInterval);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("Stopping the database worker");
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                var stopTimeStamps = startTimestamp + queryInterval.TotalMilliseconds;
                var dataStampsForProcessing = _bigDataContext.DataStamps
                    .Where(x => x.IsProcessed == false && x.Timestamp >= startTimestamp && x.Timestamp < stopTimeStamps)
                    .OrderBy(x => x.Timestamp)
                    .ToList();
                if (dataStampsForProcessing.Count != 0)
                {
                    var features = featureExtractor.ExtractStatisticalFeatures(dataStampsForProcessing);

                    var dataStampsProcessed = new DataStampsStatisticalFeaturesPredicted();
                    dataStampsProcessed.IsProcessed = false;
                    dataStampsProcessed.Label = dataStampsForProcessing.First().Label;
                    dataStampsProcessed.StartLocation = dataStampsForProcessing.First().Location;
                    dataStampsProcessed.StopLocation = dataStampsForProcessing.Last().Location;
                    dataStampsProcessed.StartTimestamp = dataStampsForProcessing.First().Timestamp;
                    dataStampsProcessed.StopTimestamp = dataStampsForProcessing.Last().Timestamp;
                    dataStampsProcessed.phoneAccelerometerStatistics = ToEntityStatisticalFeatures(features.phoneAccFeatures);
                    dataStampsProcessed.phoneGyroscopeStatistics = ToEntityStatisticalFeatures(features.phoneGyroFeatures);
                    dataStampsProcessed.sensorAccelerometerStatistics = ToEntityStatisticalFeatures(features.sensorAccFeatures);
                    dataStampsProcessed.sensorGyroscopeStatistics = ToEntityStatisticalFeatures(features.sensorGyroFeature);
                    dataStampsProcessed.stepsCount = features.totalSteps;

                    if (dataStampsForProcessing.First().Label == null)
                    {
                        // This is unlabeled data, which should be predicted and assigned a label
                        _bigDataContext.DataStampsStatisticalFeaturesPredicted.Add(dataStampsProcessed);
                    }
                    else
                    {
                        // This is labeled data, used for training the model
                        _bigDataContext.DataStampsStatisticalFeatures.Add(dataStampsProcessed);
                    }

                    // Mark the data as processed
                    foreach (DataStamp data in dataStampsForProcessing)
                    {
                        data.IsProcessed = true;
                        _bigDataContext.Update(data);
                    }
                    _bigDataContext.SaveChanges();
                }
                startTimestamp += (long)queryInterval.TotalMilliseconds;
            }
        }

        private Models.CartesianStatisticalFeatures ToEntityStatisticalFeatures(FeatureExtraction.CartesianStatisticalFeatures features)
        {
            var entityFeatures = new Models.CartesianStatisticalFeatures();
            entityFeatures.xAxisFeatures = ToEntityStatisticalFeatures(features.xAxisFeatures);
            entityFeatures.yAxisFeatures = ToEntityStatisticalFeatures(features.yAxisFeatures);
            entityFeatures.zAxisFeatures = ToEntityStatisticalFeatures(features.zAxisFeatures);
            return entityFeatures;
        }

        private Models.StatisticalFeatures ToEntityStatisticalFeatures(FeatureExtraction.StatisticalFeatures features)
        {
            var entityFeatures = new Models.StatisticalFeatures();
            entityFeatures.Max = features.Max;
            entityFeatures.Min = features.Min;
            entityFeatures.Mean = features.Mean;
            entityFeatures.StandardDeviation = features.StandardDeviation;
            return entityFeatures;
        }
    }
}
