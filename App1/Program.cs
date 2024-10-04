using App1.Models;
using Newtonsoft.Json;
using iText.Kernel.Pdf;
using System.Text;
using iText.Kernel.Pdf.Canvas.Parser;

namespace App1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string pdfPath = "SampleNetworkVulnerabilityScanReport.pdf";
            string text = ExtractTextFromPdf(pdfPath);

            Console.WriteLine(text);

            //string apiUrl = "https://localhost:7227/api/v1/data";
            //await SendPostRequest(apiUrl, new DataModel { Data = text });
        }

        private static string ExtractTextFromPdf(string pdfPath)
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            {
                string res = "";
                for (int i = 1; i != pdfDocument.GetNumberOfPages(); i++)
                {
                    res += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
                }
                return res;
            }
        }

        public static async Task SendPostRequest(string apiUrl, DataModel data)
        {
            using (HttpClient client = new HttpClient()) 
            {
                string json = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"POST response : {result}");
            }
        }
    }
}
