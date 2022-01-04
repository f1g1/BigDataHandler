using Microsoft.AspNetCore.Mvc;
using System;
using BigDataHandler.Models;
using BigDataHandler.EFConfigs;
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

        [HttpGet]
        [Route("/api/data/trainingData")]
        public IEnumerable<DataStampsStatisticalFeatures> GetLabeledData()
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
