using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigDataHandler.FeatureExtraction;
using BigDataHandler.Models;
using System.Collections.Generic;
using System;

namespace UnitTests
{
    [TestClass]
    public class FeatureExtractorUnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("Testing the feature extraction...");

            List<DataStamp> mockedData = new();
            var data = new DataStamp();
            data.Values = "[0.1,0.2,0.4]";
            data.Type = "Accelerometer";
            data.Source = "Sensor";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[0.7,0.4,0.8]";
            data.Type = "Accelerometer";
            data.Source = "Sensor";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[0.3,0.4,0.1]";
            data.Type = "Accelerometer";
            data.Source = "Sensor";
            mockedData.Add(data);

            data = new DataStamp();
            data.Values = "[0.7,0.3,0.5]";
            data.Type = "Gyroscope";
            data.Source = "Sensor";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[2.8,1.3,0.9]";
            data.Type = "Gyroscope";
            data.Source = "Sensor";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[0.2,2.4,1.1]";
            data.Type = "Gyroscope";
            data.Source = "Sensor";
            mockedData.Add(data);

            data = new DataStamp();
            data.Values = "[5.9,8.4,4.2]";
            data.Type = "Gyroscope";
            data.Source = "Phone";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[0.1,6.2,7.1]";
            data.Type = "Gyroscope";
            data.Source = "Phone";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[7.1,5.4,4.9]";
            data.Type = "Gyroscope";
            data.Source = "Phone";
            mockedData.Add(data);

            data = new DataStamp();
            data.Values = "[1.5,6.5,1.2]";
            data.Type = "Accelerometer";
            data.Source = "Phone";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[1.7,6.0,1.9]";
            data.Type = "Accelerometer";
            data.Source = "Phone";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[3.3,8.7,1.1]";
            data.Type = "Accelerometer";
            data.Source = "Phone";
            mockedData.Add(data);

            data = new DataStamp();
            data.Values = "[345]";
            data.Type = "Steps";
            data.Source = "Phone";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[642]";
            data.Type = "Steps";
            data.Source = "Phone";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[1087]";
            data.Type = "Steps";
            data.Source = "Phone";
            mockedData.Add(data);

            var featureExtractor = new StatisticalFeatureExtractor();
            var features = featureExtractor.ExtractStatisticalFeatures(mockedData);

            // Sensor Accelerometer checks
            {
                Assert.AreEqual(0.7, features.sensorAccFeatures.xAxisFeatures.Max);
                Assert.AreEqual(0.1, features.sensorAccFeatures.xAxisFeatures.Min);
                Assert.AreEqual(0.36, features.sensorAccFeatures.xAxisFeatures.Mean, 0.01);
                Assert.AreEqual(0.24, features.sensorAccFeatures.xAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(0.4, features.sensorAccFeatures.yAxisFeatures.Max);
                Assert.AreEqual(0.2, features.sensorAccFeatures.yAxisFeatures.Min);
                Assert.AreEqual(0.333, features.sensorAccFeatures.yAxisFeatures.Mean, 0.01);
                Assert.AreEqual(0.094, features.sensorAccFeatures.yAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(0.8, features.sensorAccFeatures.zAxisFeatures.Max);
                Assert.AreEqual(0.1, features.sensorAccFeatures.zAxisFeatures.Min);
                Assert.AreEqual(0.43, features.sensorAccFeatures.zAxisFeatures.Mean, 0.01);
                Assert.AreEqual(0.286, features.sensorAccFeatures.zAxisFeatures.StandardDeviation, 0.01);
            }

            //Sensor Gyroscope checks
            {
                Assert.AreEqual(2.8, features.sensorGyroFeature.xAxisFeatures.Max);
                Assert.AreEqual(0.2, features.sensorGyroFeature.xAxisFeatures.Min);
                Assert.AreEqual(1.23, features.sensorGyroFeature.xAxisFeatures.Mean, 0.01);
                Assert.AreEqual(1.12, features.sensorGyroFeature.xAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(2.4, features.sensorGyroFeature.yAxisFeatures.Max);
                Assert.AreEqual(0.3, features.sensorGyroFeature.yAxisFeatures.Min);
                Assert.AreEqual(1.333, features.sensorGyroFeature.yAxisFeatures.Mean, 0.01);
                Assert.AreEqual(0.85, features.sensorGyroFeature.yAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(1.1, features.sensorGyroFeature.zAxisFeatures.Max);
                Assert.AreEqual(0.5, features.sensorGyroFeature.zAxisFeatures.Min);
                Assert.AreEqual(0.83, features.sensorGyroFeature.zAxisFeatures.Mean, 0.01);
                Assert.AreEqual(0.24, features.sensorGyroFeature.zAxisFeatures.StandardDeviation, 0.01);
            }

            // Phone Accelerometer checks
            {
                Assert.AreEqual(3.3, features.phoneAccFeatures.xAxisFeatures.Max);
                Assert.AreEqual(1.5, features.phoneAccFeatures.xAxisFeatures.Min);
                Assert.AreEqual(2.16, features.phoneAccFeatures.xAxisFeatures.Mean, 0.01);
                Assert.AreEqual(0.80, features.phoneAccFeatures.xAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(8.7, features.phoneAccFeatures.yAxisFeatures.Max);
                Assert.AreEqual(6.0, features.phoneAccFeatures.yAxisFeatures.Min);
                Assert.AreEqual(7.06, features.phoneAccFeatures.yAxisFeatures.Mean, 0.01);
                Assert.AreEqual(1.17, features.phoneAccFeatures.yAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(1.9, features.phoneAccFeatures.zAxisFeatures.Max);
                Assert.AreEqual(1.1, features.phoneAccFeatures.zAxisFeatures.Min);
                Assert.AreEqual(1.4, features.phoneAccFeatures.zAxisFeatures.Mean, 0.01);
                Assert.AreEqual(0.35, features.phoneAccFeatures.zAxisFeatures.StandardDeviation, 0.01);
            }

            // Phone Gyroscope checks
            {
                Assert.AreEqual(7.1, features.phoneGyroFeatures.xAxisFeatures.Max);
                Assert.AreEqual(0.1, features.phoneGyroFeatures.xAxisFeatures.Min);
                Assert.AreEqual(4.36, features.phoneGyroFeatures.xAxisFeatures.Mean, 0.01);
                Assert.AreEqual(3.05, features.phoneGyroFeatures.xAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(8.4, features.phoneGyroFeatures.yAxisFeatures.Max);
                Assert.AreEqual(5.4, features.phoneGyroFeatures.yAxisFeatures.Min);
                Assert.AreEqual(6.66, features.phoneGyroFeatures.yAxisFeatures.Mean, 0.01);
                Assert.AreEqual(1.26, features.phoneGyroFeatures.yAxisFeatures.StandardDeviation, 0.01);

                Assert.AreEqual(7.1, features.phoneGyroFeatures.zAxisFeatures.Max);
                Assert.AreEqual(4.2, features.phoneGyroFeatures.zAxisFeatures.Min);
                Assert.AreEqual(5.4, features.phoneGyroFeatures.zAxisFeatures.Mean, 0.01);
                Assert.AreEqual(1.23, features.phoneGyroFeatures.zAxisFeatures.StandardDeviation, 0.01);
            }

            // Steps checks
            Assert.AreEqual(1087, features.totalSteps);

            Console.WriteLine("Finished testing the feature extraction!");
        }
    }
}
