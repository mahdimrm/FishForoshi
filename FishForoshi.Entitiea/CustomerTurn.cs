using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class CustomerTurn : BaseEntity
    {
        [Required]
        [Display(Name = "نام آرایشگر")]
        public Guid BarberStaffId { get; set; }

        [Required]
        [Display(Name = "نام مشتری")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "ساعت")]
        public DateTime CustomerQueueTime { get; set; }


        public virtual Barber BarberStaff { get; set; }
    }
    public enum CustomerTurnActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2,
    }
}
