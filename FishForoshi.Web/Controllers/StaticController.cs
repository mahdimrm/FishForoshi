using ClosedXML.Excel;
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
            using (var workbook = new XLWorkbook(@"G:/DotNetProjects/FishForoshi/FishForoshi.Web/wwwroot/Files/Norm.xlsx"))
            {
                var worksheet = workbook.Worksheet("sheet1");

                //Ranges 
                var daterange = worksheet.Cell("G1").Value = $"  مورخ  : {date}";
                var headerRanges = worksheet.Cells("B3,D3,F3,H3,J3,L3");
                var matterRanges = worksheet.Cells("B7,D7,F7,H7,J7,L7");
                var normRanges = worksheet.Cells("C7,E7,G7,I7,K7,M7");
                var calculatedNormsRanges = worksheet.Cells("B9,D9,F9,H9,J9,L9");

                //Indexes

                int headerRangeIndex = 0;
                var currentRow = 7;
                int index = 0;

                foreach (var item in headerRanges)
                {
                    item.Value = $"{statistics[headerRangeIndex].FoodName} ({statistics[headerRangeIndex].FoodCount})";
                    headerRangeIndex++;
                }

                foreach (var item in normRanges)
                {
                    foreach (var norm in statistics[index].Norms)
                    {
                        worksheet.Cell(currentRow, item.WorksheetColumn().ColumnLetter()).Value = norm.Value;
                        currentRow += 4;
                    }
                    currentRow = 7;
                    index++;
                }

                index = 0;

                foreach (var range in matterRanges)
                {
                    foreach (var norm in statistics[index].Norms)
                    {
                        worksheet.Cell(currentRow, range.WorksheetColumn().ColumnLetter()).Value = norm.Name;
                        currentRow += 4;
                    }
                    currentRow = 7;
                    index++;
                }

                index = 0;
                currentRow = 9;

                foreach (var range in calculatedNormsRanges)
                {
                    foreach (var norm in statistics[index].CalculatedNorms)
                    {
                        worksheet.Cell(currentRow, range.WorksheetColumn().ColumnLetter()).Value = norm.Value.ToString("#,##0");
                        currentRow += 4;
                    }
                    currentRow = 9;
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
