using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigDataHandler.FeatureExtraction;
using BigDataHandler.Models;

namespace BigDataHandler.FeatureExtraction
{
    public class StatisticalFeatureExtractor
    {
        public StatisticalFeatureExtractor()
        {
        }

        public CartesianStatisticalFeatures ExtractStatisticalFeatures(List<DataStamp> dataStamps) {
            CartesianStatisticalFeatures features = new();

            List<double> xValues = new();
            List<double> yValues = new();
            List<double> zValues = new();

            foreach(DataStamp data in dataStamps)
            {
                var axisValues = ExtractAxisValues(data);
                xValues.Add(axisValues[0]);
                yValues.Add(axisValues[1]);
                zValues.Add(axisValues[2]);
            }

            ExtractFeatures(xValues, features.xAxisFeatures);
            ExtractFeatures(yValues, features.yAxisFeatures);
            ExtractFeatures(zValues, features.zAxisFeatures);
            return features;
        }

        private void ExtractFeatures(List<double> values, StatisticalFeatures features)
        {
            features.Max = ExtractMax(values);
            features.Min = ExtractMin(values);
            features.Mean = ExtractMean(values);
            features.StandardDeviation = ExtractStandardDeviation(values);
        }

        private List<double> ExtractAxisValues(DataStamp dataStamp)
        {
            List<double> parsedValues = new();
            var values = dataStamp.Values;
            values = values.Substring(1, values.Length - 2);
            var stringValues = values.Split(',');
            foreach(string value in stringValues)
            {
                parsedValues.Add(Double.Parse(value));
            }
            return parsedValues;
        }

        private double ExtractMin(List<double> values)
        {
            return values.Min();
        }

        private double ExtractMax(List<double> values)
        {
            return values.Max();
        }

        private double ExtractMean(List<double> values)
        {
            return values.Average();
        }

        private double ExtractStandardDeviation(List<double> values)
        {
            var mean = values.Average();
            var standardDeviation = 0.0;
            foreach(double value in values)
            {
                standardDeviation += Math.Pow(value - mean, 2);
            }
            standardDeviation = standardDeviation / values.Count;
            standardDeviation = Math.Sqrt(standardDeviation);
            return standardDeviation;
        }
    }
}
