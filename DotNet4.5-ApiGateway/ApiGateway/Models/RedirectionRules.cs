namespace ApiGateway.Models
{
    public class RedirectionRules
    {
        public string EndPoint { get; set; }
        public bool IsDefault { get; set; }
        public string DestinationUrl { get; set; }
    }
}