﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL_NET2.Models;

namespace BTL_NET2.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    if (HttpContext.Session.GetString("UserId") != null)
    {
      return View();
    }
    return RedirectToAction("Login", "User");
  }

  // public IActionResult Privacy()
  // {
  //     return View();
  // }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
