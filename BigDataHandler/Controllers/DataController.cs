using Microsoft.AspNetCore.Mvc;
using System;
using BigDataHandler.Models;
using BigDataHandler.EFConfigs;
using BigDataHandler.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BigDataHandler.FeatureExtraction;

namespace BigDataHandler.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private StatisticalFeatureExtractor featureExtractor;

        public DataController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            featureExtractor = new StatisticalFeatureExtractor();

        }

        [HttpPost]
        [Route("/api/data/labelData")]
        public string LabelData(DataLabelingInformation labelingInformation)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var dataToLabel = _bigDataContext.DataStampsStatisticalFeaturesPredicted.Where(d => d.Id == labelingInformation.Id).First();
                    if (dataToLabel == null) return "Failed to update the label, data with id:" + labelingInformation.Id + "does not exist!";
                    dataToLabel.Label = labelingInformation.Label;
                    dataToLabel.IsProcessed = true;
                    _bigDataContext.Update(dataToLabel);
                    _bigDataContext.SaveChanges();
                    return "Sucessfully labele data!";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return "Failed to update label due to an exception!";
        }

        [HttpGet]
        [Route("/api/data/trainingData")]
        public IEnumerable<DataStampsStatisticalFeatures> GetTrainingData()
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeatures
                            .Where(d => d.IsProcessed == false && d.Label != null);
                    var data = IncludeAllEntities(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<DataStampsStatisticalFeatures>();
        }

        [HttpGet]
        [Route("/api/data/labelingData")]
        public IEnumerable<DataStampsStatisticalFeatures> GetLabelingData()
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeatures
                            .Where(d => d.IsProcessed == false && d.Label == null);
                    var data = IncludeAllEntities(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<DataStampsStatisticalFeatures>();
        }
        [HttpGet]
        [Route("/api/data/activityStartTraining")]
        public IEnumerable<long> GetActivityStartsTraining()
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeatures
                            .Select(x => x.ActivityStartTimestamp).Distinct();

                    return query.ToArray();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<long>();
        }
        [HttpGet]
        [Route("/api/data/activityStartLabeling")]
        public IEnumerable<long> GetActivityStartsLabeling()
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeaturesPredicted
                            .Select(x => x.ActivityStartTimestamp).Distinct();

                    return query.ToArray();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<long>();
        }


        [HttpGet]
        [Route("/api/data/labeledData")]
        public IEnumerable<DataStampsStatisticalFeatures> GetLabeledData(long startTimeStamp, long stopTimeStamp, string label)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeatures
                            .Where(d => d.IsProcessed == false && d.StartTimestamp >= startTimeStamp && d.StopTimestamp <= stopTimeStamp);
                    if (label != null)
                    {
                        query = query.Where(d => d.Label == label);
                    }
                    var data = IncludeAllEntities(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<DataStampsStatisticalFeatures>();
        }


        [HttpGet]
        [Route("/api/data/labeledDataByActivity")]
        public IEnumerable<DataStampsStatisticalFeatures> GetLabeledDataByActivity(long activityTimeStamp)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeatures
                        .Where(d => d.IsProcessed == true && d.ActivityStartTimestamp == activityTimeStamp);
                    var data = IncludeAllEntities(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<DataStampsStatisticalFeatures>();
        }

        [HttpGet]
        [Route("/api/data/classifiedData")]
        public IEnumerable<DataStampsStatisticalFeaturesPredicted> GetClassifiedData(long startTimeStamp, long stopTimeStamp, string label)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeaturesPredicted
                            .Where(d => d.IsProcessed == true && d.StartTimestamp >= startTimeStamp && d.StopTimestamp <= stopTimeStamp);
                    if (label != null)
                    {
                        query = query.Where(d => d.Label == label);
                    }
                    var data = IncludeAllEntities(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<DataStampsStatisticalFeaturesPredicted>();
        }

        [HttpGet]
        [Route("/api/data/classifiedDataByActivity")]
        public IEnumerable<DataStampsStatisticalFeaturesPredicted> GetClassifiedDataByActivity(long activityTimeStamp)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStampsStatisticalFeaturesPredicted
                        .Where(d => d.IsProcessed == true && d.ActivityStartTimestamp == activityTimeStamp);
                    var data = IncludeAllEntities(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return new List<DataStampsStatisticalFeaturesPredicted>();
        }

        [HttpGet]
        [Route("/api/data/manualTriggerStats")]
        public string ManualTriggerStats()
        {
            TimeSpan queryInterval = TimeSpan.FromSeconds(10);

            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    BigDataContext _bigDataContext = scope.ServiceProvider.GetRequiredService<BigDataContext>();
                    var query = _bigDataContext.DataStamps
                            .Where(d => d.IsProcessed == false).OrderBy(x => x.Timestamp).ToList();

                    if (query.Count() == 0) return "no messages";

                    var initTimestamp = query[0].Timestamp;

                    List<DataStamp> tenSecondBatch = new List<DataStamp>();
                    do
                    {
                        long stop = initTimestamp + (long)queryInterval.TotalMilliseconds;

                        tenSecondBatch = query.Where(x => x.Timestamp >= initTimestamp && x.Timestamp < stop).ToList();

                        var features = featureExtractor.ExtractStatisticalFeatures(tenSecondBatch);

                        var dataStampsProcessed = new DataStampsStatisticalFeaturesPredicted();
                        dataStampsProcessed.IsProcessed = false;
                        dataStampsProcessed.Label = tenSecondBatch.First().Label;
                        dataStampsProcessed.StartLocation = tenSecondBatch.First().Location;
                        dataStampsProcessed.StopLocation = tenSecondBatch.Last().Location;
                        dataStampsProcessed.ActivityStartTimestamp = tenSecondBatch.First().ActivityStartTimestamp;
                        dataStampsProcessed.StartTimestamp = tenSecondBatch.First().Timestamp;
                        dataStampsProcessed.StopTimestamp = tenSecondBatch.Last().Timestamp;
                        dataStampsProcessed.phoneAccelerometerStatistics = features.phoneAccFeatures;
                        dataStampsProcessed.phoneGyroscopeStatistics = features.phoneGyroFeatures;
                        dataStampsProcessed.sensorAccelerometerStatistics = features.sensorAccFeatures;
                        dataStampsProcessed.sensorGyroscopeStatistics = features.sensorGyroFeature;
                        dataStampsProcessed.stepsCount = features.totalSteps;

                        if (tenSecondBatch.First().Label == null)
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
                        foreach (DataStamp data in tenSecondBatch)
                        {
                            data.IsProcessed = true;
                            _bigDataContext.Update(data);
                        }
                        _bigDataContext.SaveChanges();

                        initTimestamp = stop;

                    } while (tenSecondBatch.Count > 0);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return "SUCCESS";
        }


        static private IQueryable<T> IncludeAllEntities<T>(IQueryable<T> query) where T : DataStampsStatisticalFeatures
        {
            return query.Include(d => d.phoneAccelerometerStatistics.xAxisFeatures)
                .Include(d => d.phoneAccelerometerStatistics.yAxisFeatures)
                .Include(d => d.phoneAccelerometerStatistics.zAxisFeatures)
                .Include(d => d.phoneGyroscopeStatistics.xAxisFeatures)
                .Include(d => d.phoneGyroscopeStatistics.yAxisFeatures)
                .Include(d => d.phoneGyroscopeStatistics.zAxisFeatures)
                .Include(d => d.sensorAccelerometerStatistics.xAxisFeatures)
                .Include(d => d.sensorAccelerometerStatistics.yAxisFeatures)
                .Include(d => d.sensorAccelerometerStatistics.zAxisFeatures)
                .Include(d => d.sensorGyroscopeStatistics.xAxisFeatures)
                .Include(d => d.sensorGyroscopeStatistics.yAxisFeatures)
                .Include(d => d.sensorGyroscopeStatistics.zAxisFeatures);
        }
    }
}
