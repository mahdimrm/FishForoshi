using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class PrintController : Controller
    {
        public IActionResult PrintFood()
        {

            return View();
        }
    }
}
