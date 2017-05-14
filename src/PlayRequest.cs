using Newtonsoft.Json;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class PlayRequest
    {
        [JsonProperty("frameRate")]
        public double FrameRate { get; set; }
    }
}
