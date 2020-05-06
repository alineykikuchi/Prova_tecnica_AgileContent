using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTaas.Models
{
    public class MinhaCDN
    {
        public string Provider { get; set; }

        public string HttpMethod { get; set; }

        public int StatusCode { get; set; }

        public string UriPath { get; set; }

        public int TimeTaken { get; set; }

        public int ResponseSize { get; set; }

        public string CacheSatus { get; set; }
    }
}
