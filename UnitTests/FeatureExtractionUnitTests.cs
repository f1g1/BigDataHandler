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
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[0.7,0.4,0.8]";
            mockedData.Add(data);
            data = new DataStamp();
            data.Values = "[0.3,0.4,0.1]";
            mockedData.Add(data);

            var featureExtractor = new StatisticalFeatureExtractor();
            var features = featureExtractor.ExtractStatisticalFeatures(mockedData);

            Assert.AreEqual(0.7, features.xAxisFeatures.Max);
            Assert.AreEqual(0.1, features.xAxisFeatures.Min);
            Assert.AreEqual(0.36, features.xAxisFeatures.Mean, 0.01);
            Assert.AreEqual(0.24, features.xAxisFeatures.StandardDeviation, 0.01);

            Assert.AreEqual(0.4, features.yAxisFeatures.Max);
            Assert.AreEqual(0.2, features.yAxisFeatures.Min);
            Assert.AreEqual(0.333, features.yAxisFeatures.Mean, 0.01);
            Assert.AreEqual(0.094, features.yAxisFeatures.StandardDeviation, 0.01);

            Assert.AreEqual(0.8, features.zAxisFeatures.Max);
            Assert.AreEqual(0.1, features.zAxisFeatures.Min);
            Assert.AreEqual(0.43, features.zAxisFeatures.Mean, 0.01);
            Assert.AreEqual(0.286, features.zAxisFeatures.StandardDeviation, 0.01);

            Console.WriteLine("Finished testing the feature extraction!");
        }
    }
}
