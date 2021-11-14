
namespace BigDataHandler.Models
{
    public record DataStamp
    {
        public long Timestamp { get; set; }
        public string Type { get; set; }
        public object Values { get; set; }
    }
}
