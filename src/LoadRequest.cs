using Newtonsoft.Json;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class LoadRequest
    {
        [JsonProperty("frames")]
        public int[][] Frames { get; set; }
    }
}
