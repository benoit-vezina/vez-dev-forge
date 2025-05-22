using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PerfectRestApi.Repository.Entities;
using PerfectRestApi.Services;

namespace PerfectRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CookieController(ILogger<CookieController> logger, ICookieService cookieService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cookie>>> GetAll(
        [FromQuery] string? name, 
        [FromQuery] double? minSugar, 
        [FromQuery] double? maxSugar)
    {
        logger.LogInformation("Getting cookies with filters: Name={Name}, MinSugar={MinSugar}, MaxSugar={MaxSugar}", 
            name, minSugar, maxSugar);
            
        // If no filters are provided, return all cookies
        if (string.IsNullOrEmpty(name) && !minSugar.HasValue && !maxSugar.HasValue)
        {
            return Ok(await cookieService.GetAllCookiesAsync());
        }
        
        // Otherwise, apply filters
        return Ok(await cookieService.GetFilteredCookiesAsync(name, minSugar, maxSugar));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cookie>> GetById(Guid id)
    {
        var cookie = await cookieService.GetCookieByIdAsync(id);
        if (cookie == null)
            return NotFound();
            
        return Ok(cookie);
    }

    [HttpPost]
    public async Task<ActionResult<Cookie>> Create(Cookie cookie)
    {
        var created = await cookieService.CreateCookieAsync(cookie);
        return CreatedAtAction(nameof(GetById), new { id = created.CookieId }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Cookie>> Update(Guid id, Cookie cookie)
    {
        var updated = await cookieService.UpdateCookieAsync(id, cookie);
        if (updated == null)
            return NotFound();
            
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await cookieService.DeleteCookieAsync(id);
        if (!result)
            return NotFound();
            
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Cookie>> Patch(Guid id, [FromBody] JsonPatchDocument<Cookie> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest("No patch document provided");
        }
        
        var updated = await cookieService.PatchCookieAsync(id, patchDoc);
        if (updated == null)
            return NotFound();
            
        return Ok(updated);
    }
}