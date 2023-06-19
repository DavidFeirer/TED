namespace QueueService.Model
{
    public class Message <T>
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public HttpMethod HttpMethode { get; set; }
        public T Inhalt { get; set; }
    }
}
