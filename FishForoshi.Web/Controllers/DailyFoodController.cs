using FishForoshi.Abstraction;
using FishForoshi.Entities;
using FishForoshi.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FishForoshi.Web.Controllers
{
    public class DailyFoodController : BaseController
    {
        private readonly IGetDailyFood _query;
        private readonly IDailyFoodAction _action;
        private readonly IGetFood _foodQuery;
        private readonly IGetDay _day;

        public DailyFoodController(IGetDailyFood query,
            IDailyFoodAction action, IGetFood foodQuery, IGetDay day)
        {
            _query = query;
            _action = action;
            _foodQuery = foodQuery;
            _day = day;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _query.GetAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var foods = MapSelectListItems(await _foodQuery.GetNames());
            ViewBag.Foods = foods;

            var days = MapSelectListItems(await _day.GetNames());
            ViewBag.Days = days;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DailyFood dailyFood)
        {
            var result = await _action.CreateAsync(dailyFood);
            return DailyActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var foods = MapSelectListItems(await _foodQuery.GetNames());
            ViewBag.Foods = foods;

            var days = MapSelectListItems(await _day.GetNames());
            ViewBag.Days = days;

            var day = await _query.GetAsync(id);
            if (day is null)
            {
                TempData[ErrorMessage] = "یافت نشد";
                return RedirectToAction("Index");
            }
            return View(day);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DailyFood dailyFood)
        {
            var result = await _action.UpdateAsync(dailyFood);
            return DailyActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var daily = await _query.GetAsync(id);
            if (daily is null)
            {
                return NotFound();
            }
            return View(daily);
        }

        public async Task<IActionResult> ConfirmationDelete(Guid id)
        {
            var result = await _action.DeleteAsync(id);
            return DailyActionResult(result);
        }

        public IActionResult DailyActionResult(DailyFoodStatus status)
        {
            switch (status)
            {
                case DailyFoodStatus.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                    return RedirectToAction("Index");

                case DailyFoodStatus.Failed:
                    TempData[ErrorMessage] = "خطا";
                    return RedirectToAction("Index");

                case DailyFoodStatus.HaveFactor:
                    TempData[InfoMessage] = "برای این فاکاور فیش فروخته شده است";
                    return RedirectToAction("Index");
                default:
                    TempData[ErrorMessage] = "دوباره امتحان کنید";
                    return RedirectToAction("Index");
            }
        }

        public IEnumerable<SelectListItem> MapSelectListItems(IEnumerable<SelectListDto> input)
        {
            List<SelectListItem> result = new();
            foreach (var item in input)
            {
                result.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

            return result;
        }
    }
}

