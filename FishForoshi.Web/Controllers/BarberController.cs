using FishForoshi.Abstraction;
using FishForoshi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class BarberController : BaseController
    {
        private readonly IGetBarber _query;
        private readonly IBarberAction _action;

        public BarberController(IGetBarber query, IBarberAction action)
        {
            _query = query;
            _action = action;
        }

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
        public async Task<IActionResult> Create(Barber barber)
        {
            var result = await _action.CreateAsync(barber);
            return BarberActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var barber = await _query.GetAsync(id);
            if (barber is null)
            {
                TempData[ErrorMessage] = "یافت نشد";
                return RedirectToAction("Index");
            }
            return View(barber);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Barber barber)
        {
            var result = await _action.UpdateAsync(barber);
            return BarberActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var barber = await _query.GetAsync(id);
            if (barber is null)
            {
                return NotFound();
            }
            return View(barber);
        }
        public async Task<IActionResult> ConfirmationDelete(Guid id)
        {
            var result = await _action.DeleteAsync(id);
            return BarberActionResult(result);
        }

        public IActionResult BarberActionResult(BarberActionStatus status)
        {
            switch (status)
            {
                case BarberActionStatus.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                    return RedirectToAction("Index");
                case BarberActionStatus.Failed:
                    TempData[ErrorMessage] = "خطا";
                    return RedirectToAction("Index");
                default:
                    TempData[ErrorMessage] = "دوباره امتحان کنید";
                    return RedirectToAction("Index");
            }
        }
    }
}
