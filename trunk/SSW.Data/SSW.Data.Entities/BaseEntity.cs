namespace SSW.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The base entity class that contains common fields.
    /// </summary>
    /// <typeparam name="T">
    /// Key type.
    /// </typeparam>
    public class BaseEntity<T> : IDeletableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{T}"/> class.
        /// </summary>
        public BaseEntity()
        {
            this.RowGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Key]
        public T Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when record was originally created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the record's last modified date and time.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the username of a user who originally created entity.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the username of a user who last modified record.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether record is soft deleted (marked as inactive).
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the time stamp for concurrency check.
        /// </summary>
        [Timestamp] 
        public byte[] TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the row GUID.
        /// </summary>
        /// <value>The row GUID.</value>
        public Guid RowGuid { get; set; }
    }

    /// <summary>
    /// The base entity class that contains common fields.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class BaseEntity : BaseEntity<int>
    {   
    }
}
