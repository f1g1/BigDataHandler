using BigDataHandler.Models;

namespace BigDataHandler.FeatureExtraction
{
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
