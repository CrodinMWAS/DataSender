using Microsoft.AspNetCore.Mvc;
using STRIDEAPI.Models;

namespace STRIDEAPI.Controllers
{
    [ApiController]
    [Route("api/v1/data")]
    public class DataController : ControllerBase
    {
        public static List<DataModel> DataStorage = new List<DataModel>();
        [HttpPost]
        public async Task<IActionResult> postData([FromBody] DataModel dataModel)
        {
            if (dataModel == null)
            {
                return BadRequest("Invalid Data");
            }

            // Simulate async operation
            await Task.Run(() => DataStorage.Add(dataModel));

            return Ok($"Received: {dataModel.Data}");
        }

        [HttpGet]
        public async Task<IActionResult> getData()
        {
            // Simulate async operation
            var data = await Task.Run(() => DataStorage.ToList()); // If DataStorage is a List
            DataStorage.Clear();
            return Ok(data);
        }
    }
}
