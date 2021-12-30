using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigDataHandler.FeatureExtraction
{
    public class StatisticalFeatures
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Mean { get; set; }
        public double StandardDeviation { get; set; }
    }

    public class CartesianStatisticalFeatures
    {
        public StatisticalFeatures xAxisFeatures { get; set; }
        public StatisticalFeatures yAxisFeatures { get; set; }
        public StatisticalFeatures zAxisFeatures { get; set; }
        public CartesianStatisticalFeatures()
        {
            xAxisFeatures = new();
            yAxisFeatures = new();
            zAxisFeatures = new();
        }
    }

    public class DataStampsFeatures
    {
        public CartesianStatisticalFeatures phoneGyroFeatures { get; set; }
        public CartesianStatisticalFeatures phoneAccFeatures { get; set; }
        public CartesianStatisticalFeatures sensorGyroFeature { get; set; }
        public CartesianStatisticalFeatures sensorAccFeatures { get; set; }
        public int totalSteps { get; set; }

        public DataStampsFeatures()
        {
            phoneGyroFeatures = new();
            phoneAccFeatures = new();
            sensorGyroFeature = new();
            sensorAccFeatures = new();
            totalSteps = 0;
        }
    }
}
