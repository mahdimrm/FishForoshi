using System.ComponentModel.DataAnnotations;

namespace FishForoshi.Entities
{
    public class BaseEntity
    {
        protected BaseEntity() : base()
        {
            InsertDateTime = DateTime.Now;
            IsActive = true;
        }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [Required]

        public System.DateTime InsertDateTime { get; set; }

        public System.DateTime? DeleteDateTime { get; set; }
    }
}
