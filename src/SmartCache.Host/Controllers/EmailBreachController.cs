using Microsoft.AspNetCore.Mvc;
using SmartCache.Domain.Interfaces;
using SmartCache.Domain.Models;

namespace SmartCache.Host.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailBreachController : ControllerBase
{
    private readonly IEmailBreachService _emailBreachService;

    public EmailBreachController(IEmailBreachService emailBreachService)
    {
        _emailBreachService = emailBreachService;
    }

    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> IsEmailBreachedAsync(string email)
    {
        var isEmailBreached = await _emailBreachService.IsEmailBreachedAsync(email);
        if (isEmailBreached)
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpPut("{email}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CustomExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBreachedEmailAsync(string email)
    {
        var added = await _emailBreachService.AddBreachedEmailAsync(email);

        if (added)
        {
            return StatusCode(201);
        }

        return Conflict();
    }
}
