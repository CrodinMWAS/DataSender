using App2.Models;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var data = await GetDataFromAPI("https://localhost:7227/api/v1/data");

            foreach (var item in data)
            {
                Console.WriteLine(item.Data);
            }
        }

        private static async Task<List<DataModel>> GetDataFromAPI(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                List<DataModel> data = JsonConvert.DeserializeObject<List<DataModel>>(json);
                return data;
            }
        }
    }
}
