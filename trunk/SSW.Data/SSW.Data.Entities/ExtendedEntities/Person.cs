namespace SSW.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The person object that has first and last name.
    /// </summary>
    public class Person : BaseEntity, IPersonEntity
    {
        [Required]
        [MaxLength(300)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(300)]
        public string LastName { get; set; }

        public string PreferredName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", string.IsNullOrEmpty(this.PreferredName) ? this.FirstName : this.PreferredName, this.LastName);
            }
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}", this.Id, this.FullName);
        }
    }
}
