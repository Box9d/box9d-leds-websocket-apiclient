using System;
using System.Collections.Generic;
using System.Linq;

namespace Box9.Leds.WebSocket.ApiClient.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var frames = AlternateBlackAndWhiteFrames(100, 100);

            var client = new WebSocketApiClient(new Uri("http://localhost:8003"));
            client.Load(new LoadRequest { Frames = frames }).Wait();

            if (client.IsWebsocketConnectionOpen().Result.IsConnectionOpen)
            {
                Console.WriteLine("Websocket is open");
            }

            var retrievedFrames = client.GetFrames().Result;

            client.Play(4).Wait();
        }

        static int[][] AlternateBlackAndWhiteFrames(int frameCount, int pixelCount)
        {
            var isWhite = true;
            var frames = new List<int[]>();

            for (int i = 0; i < frameCount; i++)
            {
                var rgb = isWhite ? (byte)255: (byte)0;
                frames.Add(Frame(pixelCount, rgb, rgb, rgb));
                isWhite = !isWhite;
            }

            return frames.ToArray();
        }

        static int[] Frame(int pixelCount, byte r, byte g, byte b)
        {
            var frames = new List<byte>();

            frames.Add(0);
            frames.Add(0);
            frames.Add(0);
            frames.Add(0);

            for (int i = 0; i < pixelCount; i++)
            {
                frames.Add(r);
                frames.Add(g);
                frames.Add(b);
            }

            return frames
                .Select(p => (int)p)
                .ToArray();
        }
    }
}
