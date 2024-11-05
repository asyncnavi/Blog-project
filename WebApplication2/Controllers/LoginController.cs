using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class LoginController : Controller
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Hash the password for comparison
            var passwordHash = ComputeSha256Hash(model.Password);

            // Check if user exists with matching password
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.PasswordHash == passwordHash);

            if (user != null)
            {
                // Authenticate user (for example, by setting a session or cookie)
                HttpContext.Session.SetString("Username", model.Username);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
        }

        return View(model);
    }

    private static string ComputeSha256Hash(string rawData)
    {
        using (var sha256Hash = SHA256.Create())
        {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var t in bytes) builder.Append(t.ToString("x2"));
            return builder.ToString();
        }
    }
}