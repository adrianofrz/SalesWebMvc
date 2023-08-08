using Newtonsoft.Json;

namespace SalesWebMvc.Models
{
    public class Slip
    {
        [JsonProperty("slip")]
        public Advice Advice { get; set; }
        //public int id { get; set; }
        //public string advice { get; set; }
    }
    public class Advice
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("advice")]
        public string advice { get; set; }
        
        //public Slip slip { get; set; }
    }
}
