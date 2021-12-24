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
        public CartesianStatisticalFeatures()
        {
            xAxisFeatures = new();
            yAxisFeatures = new();
            zAxisFeatures = new();
        }

        public StatisticalFeatures xAxisFeatures { get; set; }
        public StatisticalFeatures yAxisFeatures { get; set; }
        public StatisticalFeatures zAxisFeatures { get; set; }
    }
}
