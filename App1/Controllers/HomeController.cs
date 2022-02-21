using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App1.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace App1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IHttpContextAccessor _context;
    private readonly IDistributedCache _cache;
    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context, IDistributedCache cache)
    {
        _logger = logger;
        _context = context;
        _cache = cache;

    }

    public async Task<IActionResult> Index()
    {
        var user = new
        {
            Email = "bb@bb.com",
            FullName = "bb"
        };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.FullName),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
        HttpContext.Session.SetString("Name", "aaaa");

        var currentTimeUTC = DateTime.UtcNow.ToString();
        var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(20));
        await _cache.SetStringAsync("NameCache", currentTimeUTC, options);
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
