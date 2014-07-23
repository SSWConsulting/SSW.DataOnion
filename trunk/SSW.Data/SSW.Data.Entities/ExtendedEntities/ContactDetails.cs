namespace SSW.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ContactDetails : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public virtual string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Phone { get; set; }

        [MaxLength(50)]
        public virtual string Fax { get; set; }

        [MaxLength(50)]
        public virtual string Mobile { get; set; }

        public override string ToString()
        {
            return string.Format("E: {0}, P: {1}, F: {2}, M: {3}", this.Email, this.Phone, this.Fax, this.Mobile);
        }
    }
}
