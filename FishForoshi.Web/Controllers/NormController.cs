using FishForoshi.Abstraction;
using FishForoshi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class NormController : BaseController
    {
        private readonly IGetNorm _query;
        private readonly INormAction _action;
        private readonly IGetFood _foodQuery;

        public NormController(IGetNorm query, INormAction action, IGetFood foodQuery)
        {
            _query = query;
            _action = action;
            _foodQuery = foodQuery;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var food = await _foodQuery.GetByIdAsync(id);
            var result = await _query.GetNormsAsync(page, name, id);
            ViewBag.foodName = food.Name;
            ViewBag.foodId = id;
            return View(result);
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            ViewBag.foodId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Norm norm)
        {
            var result = await _action.CreateAsync(norm);
            if (result == NormActionStatus.Success)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                return Redirect($"/Norm/Index?id={norm.FoodId}");
            }
            TempData[ErrorMessage] = "خطا";
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var norm = await _query.GetByIdAsync(id);
            if (norm is null)
            {
                return NotFound();
            }
            return View(norm);
        }
        public async Task<IActionResult> ConfirmationDelete(Guid id)
        {
            var result = await _action.DeleteAsync(id);
            if (result == NormActionStatus.Success)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            TempData[ErrorMessage] = "خطا";
            return View();
        }
    }
}
