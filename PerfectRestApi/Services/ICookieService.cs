using Microsoft.AspNetCore.JsonPatch;
using PerfectRestApi.Repository.Entities;

namespace PerfectRestApi.Services;

public interface ICookieService
{
    Task<IEnumerable<Cookie>> GetAllCookiesAsync();
    Task<IEnumerable<Cookie>> GetFilteredCookiesAsync(string? nameFilter, double? minSugar, double? maxSugar);
    Task<Cookie?> GetCookieByIdAsync(Guid id);
    Task<Cookie> CreateCookieAsync(Cookie cookie);
    Task<Cookie?> UpdateCookieAsync(Guid id, Cookie cookieData);
    Task<Cookie?> PatchCookieAsync(Guid id, JsonPatchDocument<Cookie> patchDoc);
    Task<bool> DeleteCookieAsync(Guid id);
}
