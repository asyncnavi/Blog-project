using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class AboutController : Controller
{
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
 
}