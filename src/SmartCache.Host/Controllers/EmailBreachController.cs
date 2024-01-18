using Microsoft.AspNetCore.Mvc;
using SmartCache.Domain.Interfaces;

namespace SmartCache.Host.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailBreachController : ControllerBase
{
    private readonly ICacheService _cacheService;

    public EmailBreachController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> IsEmailBreached(string email)
    {
        var isEmailBreached = await _cacheService.IsEmailBreached(email);
        if (isEmailBreached)
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpPut("{email}")]
    public async Task<IActionResult> AddBreachedEmail(string email)
    {
        await _cacheService.AddBreachedEmail(email);

        return Ok();
    }
}
