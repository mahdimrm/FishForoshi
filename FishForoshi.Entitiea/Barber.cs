using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class Barber : BaseEntity
    {
        [Required]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "نوع آرایشگر")]
        public byte Type { get; set; }

        public virtual List<CustomerTurn> CustomerQueues { get; set; }

    }
    public enum BarberActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2,
    }
    public enum BarberType
    {
        Soldier = 1,
        Employee = 2,
    }
}
