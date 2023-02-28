using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class Day : BaseEntity
    {
        [Display(Name = "روز")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "شماره روز")]
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
