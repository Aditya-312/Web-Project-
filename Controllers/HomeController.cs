using Microsoft.AspNetCore.Mvc;

namespace CampusCare.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => RedirectToAction("Index", "Complaints");

    public IActionResult Error() => View();
}
