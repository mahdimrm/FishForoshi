using FishForoshi.Abstraction;
using FishForoshi.Entities;
using FishForoshi.ViewModel;
using FishForoshi.ViewModel.Common;
using FishForoshi.Web.Tools;
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
            var previewFactor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");
            if (previewFactor != null)
            {
                ViewBag.TotalPrice = previewFactor.Sum(x => x.Price * x.Count).ToString("#,##0");
            }
            var result = await _query.GetAsync();
            result.PreviewFactor = previewFactor;
            return View(result);
        }

        public async Task<IActionResult> CreatePreviewFactor(Guid id)
        {
            var dailyFood = await _query.GetAsync(id);
            List<PreviewFactorViewModel> factor = new List<PreviewFactorViewModel>();

            if (SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors") == null)
            {
                factor.Add(
                    new PreviewFactorViewModel
                    {
                        Id = dailyFood.Id,
                        Count = 1,
                        Date = dailyFood.Date,
                        Day = dailyFood.Day.Name,
                        Food = dailyFood.Food.Name,
                        ImageName = dailyFood.Food.ImageName,
                        Price = dailyFood.Food.Price
                    });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);
            }

            else
            {
                factor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");
                int index = IsExsist(id);
                if (index != -1)
                {
                    factor[index].Count++;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);
                }
                else
                {
                    factor.Add(new PreviewFactorViewModel
                    {
                        Id = dailyFood.Id,
                        Count = 1,
                        Date = dailyFood.Date,
                        Day = dailyFood.Day.Name,
                        Food = dailyFood.Food.Name,
                        ImageName = dailyFood.Food.ImageName,
                        Price = dailyFood.Food.Price
                    });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Increase(Guid id)
        {
            List<PreviewFactorViewModel> factor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");
            int index = IsExsist(id);
            if (index != -1)
            {
                factor[index].Count++;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);

            return RedirectToAction("Index");
        }

        [HttpGet("Decrease")]

        public IActionResult Decrease(Guid id)
        {
            List<PreviewFactorViewModel> factor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");
            int index = IsExsist(id);
            factor[index].Count--;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);
            return RedirectToAction("Index");
        }

        [HttpGet("Delete")]
        public IActionResult DeleteFactor(Guid id)
        {
            List<PreviewFactorViewModel> factor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");

            factor.Remove(factor.Find(x => x.Id == id));
            SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);

            return RedirectToAction("Index");
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
        private int IsExsist(Guid id)
        {
            List<PreviewFactorViewModel> items = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}

