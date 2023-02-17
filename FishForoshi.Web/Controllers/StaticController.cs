using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult CadreHallStatistics()
        {
            return View();
        }
    }
}
