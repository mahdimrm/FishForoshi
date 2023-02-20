using FishForoshi.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class BarberController : Controller
    {
        private readonly IGetBarber _query;

        public BarberController(IGetBarber query)
        {
            _query = query;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _query.GetAsync();
            return View(result);
        }
    }
}
