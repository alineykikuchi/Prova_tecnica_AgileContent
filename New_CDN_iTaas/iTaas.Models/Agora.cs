
namespace iTaas.Models
{
    public class Agora
    {
        [FormatAgora("provider")]
        public string Provider { get; set; }

        [FormatAgora("http-method")]
        public string HttpMethod { get; set; }

        [FormatAgora("status-code")]
        public int StatusCode { get; set; }

        [FormatAgora("uri-path")]
        public string UriPath { get; set; }

        [FormatAgora("time-taken")]
        public int TimeTaken { get; set; }

        [FormatAgora("response-size")]
        public int ResponseSize { get; set; }

        [FormatAgora("cache-status")]
        public string CacheSatus { get; set; }
    }
}
