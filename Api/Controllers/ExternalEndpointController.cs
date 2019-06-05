using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  public class PostModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  [Route("api/[controller]")]
  [ApiController]
  public class ExternalEndpointController : ControllerBase
  {
    // GET api/externalEndpoint
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(new[] {"value1", "value2"});
    }

    // POST api/externalEndpoint
    [HttpPost]
    public IActionResult Post([FromBody] PostModel model)
    {
      if (model == null)
        return BadRequest();

      Console.WriteLine($"Received: {model.FirstName} {model.LastName}");
      return Ok();
    }
  }
}