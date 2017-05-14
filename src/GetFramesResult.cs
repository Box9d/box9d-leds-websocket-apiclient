using Newtonsoft.Json;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class GetFramesResult
    {
        [JsonProperty("frames")]
        public int[][] Frames { get; set; }
    }
}
