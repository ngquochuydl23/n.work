using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace n.work.Controllers
{
  public class OrderController : BaseApiController
  {
    
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {

    }

    [HttpPost("{id}")]
    public void Post([FromBody] string value)
    {

    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {

    }
  }
}
