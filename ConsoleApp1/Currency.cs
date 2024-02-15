using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//"r030":840,"txt":"Долар США","rate":37.6234,"cc":"USD","exchangedate":"12.02.2024"
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

