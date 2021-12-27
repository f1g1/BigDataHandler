
namespace BigDataHandler.Models
{
    public record DataStamp
    {
        public int Id { get; set; }
        public long Timestamp { get; set; }
        public string Type { get; set; }
        public string Values { get; set; }
        public string Source { get; set; }
        public string Location { get; set; }
    }
}
