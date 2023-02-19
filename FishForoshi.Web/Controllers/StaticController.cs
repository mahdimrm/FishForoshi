using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Statistic;
using FishForoshi.ViewModel.Common;
using FishForoshi.ViewModel.Statistic;
using FishForoshi.Web.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
            return Excel(result.ToList());
        }
        public IActionResult Excel(List<CadreHallStatisticViewModel> statistics)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("آمار");

                //Styles
                workbook.Style.Font.Bold = true;
                worksheet.RightToLeft = true;
                worksheet.RowHeight = 25;
                worksheet.ColumnWidth = 25;
                worksheet.SetTabColor(XLColor.Black);

                //Ranges 
                var ranges = worksheet.Cells("A2:F2");
                var countRanges = worksheet.Cells("A1:F1");

                //Indexes
                var currentRow = 3;
                int index = 0;

                foreach (var item in countRanges)
                {
                    item.Value = $"تعداد : {statistics[index].FoodCount}";
                    index++;
                }
                index = 0;
                foreach (var range in ranges)
                {
                    range.Value = $"{statistics[index].FoodName} " + $"({statistics[index].MealType})";
                    foreach (var norm in statistics[index].Norms)
                    {
                        worksheet.Cell(currentRow, range.WorksheetColumn().ColumnLetter()).Value = $"{norm.Name}" + $"{norm.Value.ToString("#,0")}";
                        currentRow++;
                    }
                    currentRow = 3;
                    index++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        $"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"{DateTime.Now.ToPersianDate()}|آمار.xlsx");
                }
            }
        }
    }
}
