using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL_NET2.Data;
using BTL_NET2.Models;

namespace BTL_NET2.Controllers
{
  public class UserController : Controller
  {
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
      _context = context;
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user, string confirm)
    {
      try
      {
        if (user.Password == confirm)
        {
          _context.Users.Add(user);
          await _context.SaveChangesAsync();
          return RedirectToAction("Index", "Home");
        }
        return View();
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
      try
      {
        var result = await _context.Users.FirstOrDefaultAsync(m => m.Name == user.Name && m.Password == user.Password);
        if (result != null)
        {
          HttpContext.Session.SetString("UserName", result.Name);
          HttpContext.Session.SetString("UserId", result.ID);
          return RedirectToAction("Index", "Home");
        }
        return View();
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Login");
    }

  }
}
