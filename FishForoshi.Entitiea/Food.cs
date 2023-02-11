using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class Food : BaseEntity
    {

        [Display(Name = "نام غذا")]
        [Required(ErrorMessage = "  لطفا {0} وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "  لطفا {0} وارد کنید")]
        public double Price { get; set; }

        [Display(Name = "نوع غذا")]
        [Required(ErrorMessage = "  لطفا {0} وارد کنید")]
        public byte FoodType { get; set; }

        public virtual List<Norm> Norms { get; set; }
    }
    public enum FoodActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2,
    }

    public enum FoodType
    {
        Soldier = 1,
        Employee = 2
    }

}
