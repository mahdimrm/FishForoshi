using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class DailyFood : BaseEntity
    {
        [Required]
        public Guid DayId { get; set; }

        [Required]
        public Guid FoodId { get; set; }

        [Required]
        public double Count { get; set; }

        //Relations 

        public virtual Day Day { get; set; }
        public virtual Food Food { get; set; }
    }
}
