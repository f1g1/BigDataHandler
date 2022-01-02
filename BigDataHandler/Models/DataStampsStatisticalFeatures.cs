
namespace BigDataHandler.Models
{

    public record DataStampsStatisticalFeatures
    {
        public int Id { get; set; }
        public long StartTimestamp { get; set; }
        public long StopTimestamp { get; set; }
        public string StartLocation { get; set; }
        public string StopLocation { get; set; }
        public CartesianStatisticalFeatures phoneAccelerometerStatistics { get; set; }
        public CartesianStatisticalFeatures phoneGyroscopeStatistics { get; set; }
        public CartesianStatisticalFeatures sensorAccelerometerStatistics { get; set; }
        public CartesianStatisticalFeatures sensorGyroscopeStatistics { get; set; }
        public int stepsCount { get; set; }
        public bool IsProcessed { get; set; }
        public string Label { get; set; }
    }

    public record DataStampsStatisticalFeaturesPredicted : DataStampsStatisticalFeatures
    {

    }

    public class CartesianStatisticalFeatures
    {
        public CartesianStatisticalFeatures()
        {
            xAxisFeatures = new();
            yAxisFeatures = new();
            zAxisFeatures = new();
        }
        public int Id { get; set; }
        public StatisticalFeatures xAxisFeatures { get; set; }
        public StatisticalFeatures yAxisFeatures { get; set; }
        public StatisticalFeatures zAxisFeatures { get; set; }
    }

    public class StatisticalFeatures
    {
        public int Id { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Mean { get; set; }
        public double StandardDeviation { get; set; }
    }
}
