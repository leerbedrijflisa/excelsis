using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Error
    {
        public string Request { get; set; }
        public int HttpResponseCode { get; set; }
        public string Method { get; set; }
        public List<string> message { get; set; } 
    }
}
