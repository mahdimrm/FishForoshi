using FishForoshi.Abstraction;
using FishForoshi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    public class NormController : BaseController
    {
        private readonly IGetNorm _query;
        private readonly INormAction _action;

        public NormController(IGetNorm query, INormAction action)
        {
            _query = query;
            _action = action;
        }

        public async Task<IActionResult> Index(int page, string name, Guid id)
        {
            var result = await _query.GetNormsAsync(page, name, id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Norm norm)
        {
            var result = await _action.CreateAsync(norm);
            return NormActionResult(result);
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
            return NormActionResult(result);
        }
        public IActionResult NormActionResult(NormActionStatus status)
        {
            switch (status)
            {
                case NormActionStatus.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                    return RedirectToAction("Index");
                case NormActionStatus.Failed:
                    TempData[ErrorMessage] = "خطا";
                    return RedirectToAction("Index");
                default:
                    TempData[ErrorMessage] = "دوباره امتحان کنید";
                    return RedirectToAction("Index");
            }
        }
    }
}
