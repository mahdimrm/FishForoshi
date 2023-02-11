using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class Norm : BaseEntity
    {
        [Display(Name = "نام")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "مقدار")]
        [Required]
        public double Value { get; set; }

        public Guid FoodId { get; set; }
        public virtual Food Food { get; set; }
    }
    public enum NormActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2,
    }
}
