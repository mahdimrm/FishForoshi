﻿using ClosedXML.Excel;
using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Statistic;
using FishForoshi.ViewModel.Common;
using FishForoshi.ViewModel.Statistic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FishForoshi.Web.Controllers
{
    public class StatisticsController : BaseController
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
            #region ViewBags
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
            #endregion

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadreHallStatistics(List<Guid> foodIds, List<int> Counts, string date)
        {
            #region ViewBag
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
            #endregion

            var result = await _statisticsQuery.GenerateCadreHallStatistics(foodIds, Counts);
            return Excel(result.ToList(), date);
        }
        public IActionResult Excel(List<CadreHallStatisticViewModel> statistics, string date)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("آمار");

                //Styles
                workbook.Style.Font.Bold = true;
                workbook.Style.Font.Italic = true;
                worksheet.RightToLeft = true;
                worksheet.RowHeight = 25;
                worksheet.ColumnWidth = 25;
                

                //Ranges 
                var ranges = worksheet.Cells("A2:F2");
                var countRanges = worksheet.Cells("A1:F1");
                worksheet.Style.Border.BottomBorder = XLBorderStyleValues.MediumDashed;

                //Indexes
                var currentRow = 3;
                int index = 0;
                int countIndex = 0;

                foreach (var item in countRanges)
                {
                    item.Value = $"تعداد : {statistics[countIndex].FoodCount}";
                    countIndex++;
                }

                foreach (var range in ranges)
                {
                    range.Value = $"{statistics[index].FoodName} " + $"({statistics[index].MealType})";
                    foreach (var norm in statistics[index].CalculatedNorms)
                    {
                        worksheet.Cell(currentRow, range.WorksheetColumn().ColumnLetter()).Value = $"{norm.Name}  " + $"  {norm.Value.ToString("#,0")}";
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
                        $"{date}|آمار.xlsx");
                }
            }
        }
    }
}
