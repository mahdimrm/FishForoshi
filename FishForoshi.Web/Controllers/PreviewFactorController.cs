using FishForoshi.Abstraction;
using FishForoshi.ViewModel;
using FishForoshi.Web.Tools;
using Microsoft.AspNetCore.Mvc;

namespace FishForoshi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviewFactorController : ControllerBase
    {
        private readonly IGetDailyFood _query;

        public PreviewFactorController(IGetDailyFood query)
        {
            _query = query;
        }

        [HttpGet("CreatePreviewFactor")]
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
                        ImageName = dailyFood.Food.ImageName
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
                        Food = dailyFood.Food.Name
                    });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);
                }
            }
            return Ok(new { status = 200, result = new { factor } });
        }

        [HttpGet("Increase")]
        public IActionResult Increase(Guid id)
        {
            List<PreviewFactorViewModel> factor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");
            int index = IsExsist(id);
            if (index != -1)
            {
                factor[index].Count++;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);

            return Ok(new { status = 200, response = factor });
        }

        [HttpGet("Decrease")]

        public IActionResult Decrease(Guid id)
        {
            List<PreviewFactorViewModel> factor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");
            int index = IsExsist(id);
            factor[index].Count--;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);
            return Ok(new { status = 200, response = factor });
        }

        [HttpGet("Delete")]
        public IActionResult Delete(Guid id)
        {
            List<PreviewFactorViewModel> factor = SessionHelper.GetObjectFromJson<List<PreviewFactorViewModel>>(HttpContext.Session, "previewFactors");

            factor.Remove(factor.Find(x => x.Id == id));
            SessionHelper.SetObjectAsJson(HttpContext.Session, "previewFactors", factor);

            return Ok(new { status = 200, response = factor });
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
