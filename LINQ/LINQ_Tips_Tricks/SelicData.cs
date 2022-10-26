using System;
using System.Text.Json.Serialization;

namespace LINQ_Tips_Tricks
{
    public class SelicData
    {
        [JsonPropertyName ("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("selic")]
        public double Selic { get; set; }
    }
}
