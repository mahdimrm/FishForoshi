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
        public int Count { get; set; }

        [Required]
        public string Date { get; set; }

        //Relations 

        public virtual Day Day { get; set; }
        public virtual Food Food { get; set; }
    }
    public enum DailyFoodStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2,
        HaveFactor = 3,
    }
}
