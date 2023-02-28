using FishForoshi.Abstraction;
using FishForoshi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class DayController : BaseController
    {
        private readonly IGetDay _query;
        private readonly IDayAction _action;

        public DayController(IGetDay query, IDayAction action)
        {
            _query = query;
            _action = action;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _query.GetAsync();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Day day)
        {
            var result = await _action.CreateAsync(day);
            return DayActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var day = await _query.GetAsync(id);
            if (day is null)
            {
                TempData[ErrorMessage] = "یافت نشد";
                return RedirectToAction("Index");
            }
            return View(day);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Day day)
        {
            var result = await _action.UpdateAsync(day);
            return DayActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var day = await _query.GetAsync(id);
            if (day is null)
            {
                return NotFound();
            }
            return View(day);
        }
        public async Task<IActionResult> ConfirmationDelete(Guid id)
        {
            var result = await _action.DeleteAsync(id);
            return DayActionResult(result);
        }

        public IActionResult DayActionResult(DayActionStatus status)
        {
            switch (status)
            {
                case DayActionStatus.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                    return RedirectToAction("Index");
                case DayActionStatus.Failed:
                    TempData[ErrorMessage] = "خطا";
                    return RedirectToAction("Index");
                default:
                    TempData[ErrorMessage] = "دوباره امتحان کنید";
                    return RedirectToAction("Index");
            }
        }
    }
}
