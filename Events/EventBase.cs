namespace hepro.tech.webservices.events
{
    public class EventBase
    {
        public string Type { get; set; }
        public object Payload { get; set; }
    }
}