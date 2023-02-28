using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class Day : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public enum DayActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2,
    }
}
