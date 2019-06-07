using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private static readonly HttpClient HttpClient = new HttpClient();
    
    // GET api/values/getFromExternal
    [HttpGet, Route("getFromExternal")]
    public async Task<IActionResult> GetFromExternal()
    {
      using(var response = await HttpClient.GetAsync("https://localhost:5001/api/externalEndpoint"))
      {
        if (!response.IsSuccessStatusCode)
          return StatusCode((int) response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserializedResponse = JsonConvert.DeserializeObject<List<string>>(responseContent);

        return Ok(deserializedResponse);
      }
    }

    // GET api/values/postToExternal
    [HttpGet, Route("postToExternal")]
    public async Task<HttpStatusCode> PostToExternal()
    {
      var postData = new
      {
        firstName = "Derek",
        lastName = "Arends"
      };

      var serializedRequest = JsonConvert.SerializeObject(postData);

      var requestBody = new  StringContent(serializedRequest);
      requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      using(var response = await HttpClient.PostAsync("https://localhost:5001/api/externalEndpoint", requestBody))
      {
        return response.StatusCode;
      }
    }
  }
}