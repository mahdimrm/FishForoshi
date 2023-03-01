using System.ComponentModel.DataAnnotations;

namespace FishForoshi.ViewModel.DailyFood
{
    public class DailyFoodViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "روز")]
        public string Day { get; set; }

        [Display(Name = "غذا")]
        public string Food { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "تعداد")]
        public double Count { get; set; }

        [Display(Name = "مانده")]
        public double Total { get; set; }


    }
}
