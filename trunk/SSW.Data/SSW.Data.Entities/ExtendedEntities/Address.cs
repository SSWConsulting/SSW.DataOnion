namespace SSW.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Address : BaseEntity
    {
        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>The address line1.</value>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        /// <value>The address line2.</value>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        /// <value>The suburb.</value>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>The postcode.</value>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return System.Text.RegularExpressions.Regex.Replace(string.Format("{0} {1} {2} {3} {4}", this.AddressLine1, this.AddressLine2, this.Suburb, this.State, this.Country), @"\s+", " ");
        }
    }
}
