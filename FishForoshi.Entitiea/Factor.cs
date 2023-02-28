using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class Factor : BaseEntity
    {

        [Required]
        public Guid DailyFoodId { get; set; }

        [Required]
        public double Price { get; set; }

        public bool Passed { get; set; }

        //Relations

        public virtual DailyFood DailyFood { get; set; }
    }

    public enum FactorActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2,
    }
}
