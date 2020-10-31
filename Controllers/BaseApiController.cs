using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Controllers
{
  [Route("nwork-api/[controller]")]
  [ApiController]
  public class BaseApiController: ControllerBase
  {
    public override OkObjectResult Ok([ActionResultObjectValue] object value)
    {
      return base.Ok(new
      {
        statusCode = base.Ok().StatusCode,
        result = value
      });
    }

    public string AuthorizationString()
    {
      return Request.Headers["Authorization"];
    }
  }
}
