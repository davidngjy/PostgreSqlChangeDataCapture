using ChangeDataCapture.WebApi.Models;
using ChangeDataCapture.WebApi.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChangeDataCapture.WebApi.Controllers;

[ApiController]
[Route("api/v1/examples")]
public class ExampleController : Controller
{
    private readonly ExampleDbContext _context;

    public ExampleController(ExampleDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var example = await _context.Examples.ToListAsync();
        return Ok(example);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, string desc)
    {
        var newExample = new Example
        {
            Name = name,
            Description = desc
        };
        _context.Examples.Add(newExample);
        await _context.SaveChangesAsync();

        return Ok(newExample);
    }
}
