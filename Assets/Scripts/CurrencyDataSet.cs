using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Money
{
    [Serializable]
    public class CurrencyDataSet
    {
        [JsonProperty("rates")]
        //public Rates Rates { get; set; }
        public Dictionary<string, double> Rates { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
    }

}