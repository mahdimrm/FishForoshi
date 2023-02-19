using ClosedXML;
using ClosedXML.Excel;
using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Statistic;
using FishForoshi.ViewModel.Common;
using FishForoshi.ViewModel.Statistic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

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
            return Excel(result);
        }
        public IActionResult Excel(IEnumerable<CadreHallStatisticViewModel> statistics)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"آمار");
                workbook.Style.Font.Bold = true;
                worksheet.RightToLeft = true;
                worksheet.RowHeight = 20;
                worksheet.ColumnWidth = 20;

                var currentRow = 1;

                var ranges = worksheet.Cells("A1:F1");

                foreach (var statistic in statistics)
                {
                    currentRow++;
                    foreach (var range in ranges)
                    {
                        range.Value = $"{statistic.FoodName} " + $"({statistic.MealType})";
                        foreach (var item in statistic.Norms)
                        {
                            worksheet.Cell(currentRow,1 ).Value = $"{item.Name} " + $"{item.Value.ToString("#,0")}";
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "آمار.xlsx");
                }
            }
        }
    }
}
