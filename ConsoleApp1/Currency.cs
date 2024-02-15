using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Currency
    {
        public int? r030 { get; set; }
        public string? txt { get; set; }
        public decimal rate { get; set; }
        public string? cc { get; set; }
        public DateTime exchangedate { get; set; }

    }
}

