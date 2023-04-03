namespace RouteAPI.Options
{
    public class ProviderOptions
    {
        public string Name { get; set; }
        public string ApiUrl { get; set; }
        public string SearchEndpoint { get; set; }
        public string PingEndpoint { get; set; }
    }

    public class Providers { 
        public IEnumerable<ProviderOptions> Options { get; set; }    
    }
}
