using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Statistic;
using FishForoshi.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FishForoshi.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IGetFood _foodQuery;
        private readonly IGetStatistic _statisticsQuery;

        public StatisticsController(IGetFood foodQuery, IGetStatistic statisticsQuery)
        {
            _foodQuery = foodQuery;
            _statisticsQuery = statisticsQuery;
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
        public async Task<IActionResult> CadreHallStatistics()
        {
            var BreakFasts = MapSelectListItems(await _foodQuery.GetBreakFastNames());
            ViewBag.BreakFasts = BreakFasts;

            var SoldierLaunchs = MapSelectListItems(await _foodQuery.GetSoldierLaunchNames());
            ViewBag.SoldierLaunchs = SoldierLaunchs;

            var EmployeeLaunchs = MapSelectListItems(await _foodQuery.GetEmployeeLaunchNames());
            ViewBag.EmployeeLaunchs = EmployeeLaunchs;

            var Snacks = MapSelectListItems(await _foodQuery.GetSnackNames());
            ViewBag.Snacks = Snacks;

            var Dinners = MapSelectListItems(await _foodQuery.GetSoldierDinndersNames());
            ViewBag.Dinners = Dinners;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadreHallStatistics(List<Guid> foodIds, List<int> Counts)
        {
            var BreakFasts = MapSelectListItems(await _foodQuery.GetBreakFastNames());
            ViewBag.BreakFasts = BreakFasts;

            var SoldierLaunchs = MapSelectListItems(await _foodQuery.GetSoldierLaunchNames());
            ViewBag.SoldierLaunchs = SoldierLaunchs;

            var EmployeeLaunchs = MapSelectListItems(await _foodQuery.GetEmployeeLaunchNames());
            ViewBag.EmployeeLaunchs = EmployeeLaunchs;

            var Snacks = MapSelectListItems(await _foodQuery.GetSnackNames());
            ViewBag.Snacks = Snacks;

            var Dinners = MapSelectListItems(await _foodQuery.GetSoldierDinndersNames());
            ViewBag.Dinners = Dinners;

            var result = await _statisticsQuery.GenerateCadreHallStatistics(foodIds, Counts);
            return View(result);
        }
    }
}
