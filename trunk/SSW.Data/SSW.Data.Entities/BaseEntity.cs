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
    public class BaseEntity<T> : IDeletableEntity, IEquatable<BaseEntity<T>>
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

        /// <summary>
        /// Compares  two entities
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>True if there same, false otherwise</returns>
        public bool Equals(BaseEntity<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Compares two entities
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>True if there same, false otherwise</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            return Equals((BaseEntity<T>)other);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(BaseEntity<T> x, BaseEntity<T> y)
        {
            if (!ReferenceEquals(null, x)) return x.Equals(y);
            if (ReferenceEquals(null, y)) return true;
            return false;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(BaseEntity<T> x, BaseEntity<T> y)
        {
            return !(x == y);
        }
    }

    /// <summary>
    /// The base entity class that contains common fields.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class BaseEntity : BaseEntity<int>
    {   
    }
}
