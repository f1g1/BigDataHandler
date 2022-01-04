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

namespace BigDataHandler.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public DataController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
                    if(dataToLabel == null) return "Failed to update the label, data with id:" + labelingInformation.Id + "does not exist!";
                    dataToLabel.Label = labelingInformation.Label;
                    dataToLabel.IsProcessed = true;
                    _bigDataContext.Update(dataToLabel);
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
