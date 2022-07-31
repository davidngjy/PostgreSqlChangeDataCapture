using Microsoft.AspNetCore.Mvc;

namespace ChangeDataCapture.WebApi.Controllers;

[ApiController]
[Route("api/v1/examples")]
public class ExampleController : Controller
{
    [HttpGet("Test")]
    public IActionResult Test() => Ok("Endpoint is alive!");
}
