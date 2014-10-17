namespace SSW.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base entity with enforced name
    /// </summary>
    public class NamedEntity : BaseEntity, INamedEntity
    {
        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        [MaxLength(500)]
        public string Name { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Id: {0}; Name: {1};", this.Id, this.Name);
        }
    }
}
