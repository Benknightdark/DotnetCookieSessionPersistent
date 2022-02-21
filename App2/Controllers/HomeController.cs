using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App2.Models;
using Microsoft.AspNetCore.Authorization;

namespace App2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IHttpContextAccessor _context;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        _logger.LogInformation(_context.HttpContext!.Session.GetString("Name")!.ToString());
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
