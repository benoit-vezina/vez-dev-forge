using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PerfectRestApi.Repository;
using PerfectRestApi.Repository.Entities;

namespace PerfectRestApi.Services;

public class CookieService : ICookieService
{
    private readonly MyContext _context;

    public CookieService(MyContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cookie>> GetAllCookiesAsync()
    {
        return await _context.Cookies.ToListAsync();
    }
    
    public async Task<IEnumerable<Cookie>> GetFilteredCookiesAsync(string? nameFilter, double? minSugar, double? maxSugar)
    {
        IQueryable<Cookie> query = _context.Cookies;
        
        if (!string.IsNullOrEmpty(nameFilter))
        {
            query = query.Where(c => c.Name.Contains(nameFilter));
        }
        
        if (minSugar.HasValue)
        {
            query = query.Where(c => c.Sugar >= minSugar.Value);
        }
        
        if (maxSugar.HasValue)
        {
            query = query.Where(c => c.Sugar <= maxSugar.Value);
        }
        
        return await query.ToListAsync();
    }

    public async Task<Cookie?> GetCookieByIdAsync(Guid id)
    {
        return await _context.Cookies.FindAsync(id);
    }

    public async Task<Cookie> CreateCookieAsync(Cookie cookie)
    {
        cookie.CookieId = Guid.NewGuid();
        _context.Cookies.Add(cookie);
        await _context.SaveChangesAsync();
        return cookie;
    }

    public async Task<Cookie?> UpdateCookieAsync(Guid id, Cookie cookieData)
    {
        var cookie = await _context.Cookies.FindAsync(id);
        if (cookie == null)
            return null;

        cookie.Name = cookieData.Name;
        cookie.Sugar = cookieData.Sugar;

        await _context.SaveChangesAsync();
        return cookie;
    }    public async Task<bool> DeleteCookieAsync(Guid id)
    {
        var cookie = await _context.Cookies.FindAsync(id);
        if (cookie == null)
            return false;

        _context.Cookies.Remove(cookie);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<Cookie?> PatchCookieAsync(Guid id, JsonPatchDocument<Cookie> patchDoc)
    {
        var cookie = await _context.Cookies.FindAsync(id);
        if (cookie == null)
            return null;

        // Apply the JsonPatchDocument to the cookie
        patchDoc.ApplyTo(cookie);
        
        await _context.SaveChangesAsync();
        return cookie;
    }
}
