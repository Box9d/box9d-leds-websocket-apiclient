using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class WebSocketApiClient
    {
        private readonly HttpClient client;

        public WebSocketApiClient(Uri baseUri)
        {
            client = new HttpClient()
            {
                BaseAddress = baseUri
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<GetFramesResult> GetFrames()
        {
            return await this.Get<GetFramesResult>("api/frames");
        }

        public async Task Load(LoadRequest request)
        {
            await this.Post("api/load", request);
        }

        public async Task<IsWebsocketConnectionOpenResult> IsWebsocketConnectionOpen()
        {
            return await this.Get<IsWebsocketConnectionOpenResult>("api/checkConnection");
        }

        public async Task Play(int frameRate)
        {
            var request = new PlayRequest();
            request.FrameRate = frameRate;

            await this.Post("api/play", request);
        }

        internal async Task<TResponse> Get<TResponse>(string requestUri)
        {
            var response = await client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(result);
        }

        internal async Task Post<TRequest>(string requestUri, TRequest request)
        {
            var response = await client.PostAsJsonAsync(requestUri, request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
