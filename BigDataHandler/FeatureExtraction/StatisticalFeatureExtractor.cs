using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigDataHandler.FeatureExtraction;
using BigDataHandler.Models;

namespace BigDataHandler.FeatureExtraction
{
    using RawValues = Tuple<List<double>, List<double>, List<double>>;

    public class StatisticalFeatureExtractor
    {
        public StatisticalFeatureExtractor()
        {
        }

        public DataStampsFeatures ExtractStatisticalFeatures(List<DataStamp> dataStamps)
        {
            DataStampsFeatures features = new();
            
            RawValues phoneGyroRawValues = new(new List<double>(), new List<double>(), new List<double>());
            RawValues phoneAccRawValues = new(new List<double>(), new List<double>(), new List<double>());
            RawValues sensorGyroRawValues = new(new List<double>(), new List<double>(), new List<double>());
            RawValues sensorAccRawValues = new(new List<double>(), new List<double>(), new List<double>());

            foreach (DataStamp data in dataStamps)
            {
                if(data.Source == "from-phone")
                {
                    switch (data.Type)
                    {
                        case "Accelometer": ExtractRawValues(data, phoneAccRawValues); break;
                        case "Gyroscope": ExtractRawValues(data, phoneGyroRawValues); break;
                        case "Steps": features.totalSteps = ExtractStepsCounter(data); break;
                    }
                }
                else
                {
                    switch (data.Type)
                    {
                        case "Accelometer": ExtractRawValues(data, sensorAccRawValues); break;
                        case "Gyroscope": ExtractRawValues(data, sensorGyroRawValues); break;
                    }
                }
            }

            ExtractFeatures(phoneAccRawValues.Item1, features.phoneAccFeatures.xAxisFeatures);
            ExtractFeatures(phoneAccRawValues.Item2, features.phoneAccFeatures.yAxisFeatures);
            ExtractFeatures(phoneAccRawValues.Item3, features.phoneAccFeatures.zAxisFeatures);
            ExtractFeatures(phoneGyroRawValues.Item1, features.phoneGyroFeatures.xAxisFeatures);
            ExtractFeatures(phoneGyroRawValues.Item2, features.phoneGyroFeatures.yAxisFeatures);
            ExtractFeatures(phoneGyroRawValues.Item3, features.phoneGyroFeatures.zAxisFeatures);

            ExtractFeatures(sensorAccRawValues.Item1, features.sensorAccFeatures.xAxisFeatures);
            ExtractFeatures(sensorAccRawValues.Item2, features.sensorAccFeatures.yAxisFeatures);
            ExtractFeatures(sensorAccRawValues.Item3, features.sensorAccFeatures.zAxisFeatures);
            ExtractFeatures(sensorGyroRawValues.Item1, features.sensorGyroFeature.xAxisFeatures);
            ExtractFeatures(sensorGyroRawValues.Item2, features.sensorGyroFeature.yAxisFeatures);
            ExtractFeatures(sensorGyroRawValues.Item3, features.sensorGyroFeature.zAxisFeatures);
            return features;
        }

        private void ExtractRawValues(DataStamp data, RawValues rawValues)
        {
            var axisValues = ExtractAxisValues(data);
            rawValues.Item1.Add(axisValues[0]);
            rawValues.Item2.Add(axisValues[1]);
            rawValues.Item3.Add(axisValues[2]);
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

        private int ExtractStepsCounter(DataStamp dataStamp)
        {
            var values = dataStamp.Values;
            values = values.Substring(1, values.Length - 2);
            var stringValues = values.Split(',');
            return int.Parse(stringValues[0]);
        }

        private double ExtractMin(List<double> values)
        {
            if (values.Count != 0)
            {
                return values.Min();
            }
            return 0;
        }

        private double ExtractMax(List<double> values)
        {
            if (values.Count != 0)
            {
                return values.Max();
            }
            return 0;
        }

        private double ExtractMean(List<double> values)
        {
            if (values.Count != 0)
            {
                return values.Average();
            }
            return 0;
        }

        private double ExtractStandardDeviation(List<double> values)
        {
            if (values.Count != 0)
            {
                var mean = values.Average();
                var standardDeviation = 0.0;
                foreach (double value in values)
                {
                    standardDeviation += Math.Pow(value - mean, 2);
                }
                standardDeviation = standardDeviation / values.Count;
                standardDeviation = Math.Sqrt(standardDeviation);
                return standardDeviation;
            }
            return 0;
        }
    }
}
