using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace App2.Controllers;

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
        _logger.LogInformation(_context.HttpContext!.Session.GetString("Name")!.ToString());
        var encodedCachedTimeUTC = await _cache.GetAsync("NameCache");

        if (encodedCachedTimeUTC != null)
        {
            var CachedTimeUTC = Encoding.UTF8.GetString(encodedCachedTimeUTC);
            _logger.LogInformation(CachedTimeUTC);

        }

        return View();
    }
    [Authorize]

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
