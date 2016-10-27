using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace ConsoleApplication
{
    public class Program
    {
        public static object DeserializeFromStream(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize(jsonTextReader);
            }
        }

        public static void Main(string[] args)
        {
            // Build up request
            Console.WriteLine("Enter Host/IP: ");
            String host = Console.ReadLine();
            host = "192.168.1.107:8080";

            Console.WriteLine("Enter Youtube video id: ");
            String youtube_id = Console.ReadLine();
            String url = "plugin://plugin.video.youtube/?action=play_video&videoid=" + youtube_id;

            url = "http://91.195.99.70:182/d/e4zxyan647nyej54xyhm3swwvb5b7nf7dxcohqkt3huqnw2or3ps34pa/video.mp4";

            Console.WriteLine("Sending: " + url + " to host " + host);

            Request senddata = new Request
            {
                jsonrpc = "2.0",
                method = "Player.Open",
                @params = new Params {
                    item = new Item {
                        file = url
                    }
                },
                id = "1"
            };

            // Serialize
            string json = JsonConvert.SerializeObject(senddata, Formatting.Indented);

            // Send to host
            string username = "kodi";
            string password = "";

            string host_url = "http://" + username + ":" + password + "@" + host + "/jsonrpc";

            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var task = httpClient.PostAsync(host_url, content);

            var httpcontent = task.Result;

            if (!httpcontent.IsSuccessStatusCode)
            {
                Console.WriteLine("Error while sending URL");
                return;
            }
            
            Console.WriteLine("All OK!");
        }
    }
}
