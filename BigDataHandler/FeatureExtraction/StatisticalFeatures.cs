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
            phoneGyroFeatures = CartesianStatisticalFeatures.CreateEmptyFeatures();
            phoneAccFeatures = CartesianStatisticalFeatures.CreateEmptyFeatures();
            sensorGyroFeature = CartesianStatisticalFeatures.CreateEmptyFeatures();
            sensorAccFeatures = CartesianStatisticalFeatures.CreateEmptyFeatures();
            totalSteps = 0;
        }
    }
}
