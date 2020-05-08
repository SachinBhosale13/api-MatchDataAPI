using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatchDataAPI.Models
{
    public class ResponseData
    {
        public bool IsValid { get; set; }
        public string error { get; set; }
        public bool status { get; set; }
        public string ResponseMessage { get; set; }
    }
}