using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class SavedController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}