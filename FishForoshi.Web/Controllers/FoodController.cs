using FishForoshi.Abstraction;
using FishForoshi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class FoodController : BaseController
    {
        private readonly IGetFood _query;
        private readonly IFoodAction _action;
        public FoodController(IGetFood query, IFoodAction action)
        {
            _query = query;
            _action = action;
        }

        public async Task<IActionResult> Index(int page, string name)
        {
            var result = await _query.GetFoodsAsync(page, name);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Food food)
        {
            var result = await _action.CreateAsync(food);
            return FoodActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var food = await _query.GetByIdAsync(id);
            if (food is null)
            {
                TempData[ErrorMessage] = "یافت نشد";
                return RedirectToAction("Index");
            }
            return View(food);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Food food)
        {
            var result = await _action.UpdateAsync(food);
            return FoodActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var food = await _query.GetByIdAsync(id);
            if (food is null)
            {
                return NotFound();
            }
            return View(food);
        }
        public async Task<IActionResult> ConfirmationDelete(Guid id)
        {
            var result = await _action.DeleteAsync(id);
            return FoodActionResult(result);
        }

        public IActionResult FoodActionResult(FoodActionStatus status)
        {
            switch (status)
            {
                case FoodActionStatus.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                    return RedirectToAction("Index");
                case FoodActionStatus.Failed:
                    TempData[ErrorMessage] = "خطا";
                    return RedirectToAction("Index");
                default:
                    TempData[ErrorMessage] = "دوباره امتحان کنید";
                    return RedirectToAction("Index");
            }
        }

    }
}
