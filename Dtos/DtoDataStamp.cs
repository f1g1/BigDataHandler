
namespace BigDataHandler.Dtos
{
    public record DtoDataStamp
    {
        public int Id { get; set; }
        public long Timestamp { get; set; }
        public string Type { get; set; }
        public object Values { get; set; }
        public string Source { get; set; }
    }
}
