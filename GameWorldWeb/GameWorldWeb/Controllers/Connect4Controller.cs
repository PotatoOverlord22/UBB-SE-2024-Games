using Microsoft.AspNetCore.Mvc;

namespace GameWorldWeb.Controllers
{
    public class Connect4Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
