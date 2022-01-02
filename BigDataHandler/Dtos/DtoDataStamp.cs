
namespace BigDataHandler.Dtos
{
    public record DtoDataStamp
    {
        public long Timestamp { get; set; }
        public string Type { get; set; }
        public object Values { get; set; }
        public string Source { get; set; }
        public string Label { get; set; }
        public DtoLocation Location { get; set; }
    }
}
