using System;
using System.Collections.Generic;
using System.Text;

namespace SQH.Entities.Response
{
    public class CookieResponse
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public DateTime Expires { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
