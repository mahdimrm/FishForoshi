using FishForoshi.Abstraction;
using FishForoshi.Database.Migrations;
using FishForoshi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FishForoshi.Web.Controllers
{
    public class CustomerTurnController : BaseController
    {
        private readonly IGetCustomerTurn _query;
        private readonly ICustomerTurnAction _action;
        private readonly IGetBarber _barberQuery;

        public CustomerTurnController(IGetCustomerTurn query, ICustomerTurnAction action, IGetBarber barberQuery)
        {
            _query = query;
            _action = action;
            _barberQuery = barberQuery;
        }

        public async Task<IActionResult> Index(int page = 1, string? barberName = null, string? customerName = null, string? day = null)
        {
            ViewBag.barberName = barberName;
            ViewBag.customerName = customerName;
            ViewBag.day = day;
            var result = await _query.Get(page, barberName, customerName, day);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var barbers = await _barberQuery.GetAsync();
            List<SelectListItem> result = new();
            foreach (var item in barbers)
            {
                result.Add(new SelectListItem { Text = item.FullName, Value = item.Id.ToString() });
            }
            ViewBag.Barbers = result;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerTurn customerTurn)
        {
            var result = await _action.CreateAsync(customerTurn);
            if (result == CustomerTurnActionStatus.Success)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                return Redirect("/CustomerTurn/Index");
            }
            TempData[ErrorMessage] = "خطا";
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var turn = await _query.GetAsync(id);
            if (turn is null)
            {
                return NotFound();
            }
            return View(turn);
        }
        public async Task<IActionResult> ConfirmationDelete(Guid id)
        {
            var result = await _action.DeleteAsync(id);
            if (result == CustomerTurnActionStatus.Success)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد ";
                return Redirect("Index");
            }
            TempData[ErrorMessage] = "خطا";
            return View();
        }
    }
}
