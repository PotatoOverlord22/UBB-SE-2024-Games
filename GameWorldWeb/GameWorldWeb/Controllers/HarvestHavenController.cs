using Microsoft.AspNetCore.Mvc;

namespace GameWorldWeb.Controllers
{
    public class HarvestHavenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
