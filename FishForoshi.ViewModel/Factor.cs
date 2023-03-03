using System.ComponentModel.DataAnnotations;

namespace FishForoshi.ViewModel
{
    public class PreviewFactorViewModel
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }

        [Display(Name = "روز")]
        public string Day { get; set; }

        [Display(Name = "غذا")]
        public string Food { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "تعداد")]
        public double Count { get; set; }

        public double Price { get; set; }
    }
}
