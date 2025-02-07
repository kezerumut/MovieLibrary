using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDBApp.Data;
using MovieDBApp.Models;

namespace MovieDBApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext _context;
    

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _logger = logger;
     _context = context;
    }

    public async Task<IActionResult> IndexAsync()
    {
           var filmler = await _context.Movies0.ToListAsync();
            return View(filmler);
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
