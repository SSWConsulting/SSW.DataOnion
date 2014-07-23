namespace SSW.Data.Entities
{
    /// <summary>
    /// The lookup entity.
    /// </summary>
    public class LookupEntity : NamedEntity, ILookupEntity
    {
        /// <summary>
        /// Gets or sets the entity description.
        /// </summary>
        public string Description { get; set; }
    }
}
