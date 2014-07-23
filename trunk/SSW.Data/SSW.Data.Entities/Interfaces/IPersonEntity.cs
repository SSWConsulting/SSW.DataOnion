namespace SSW.Data.Entities
{
    public interface IPersonEntity
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string PreferredName { get; set; }

        string FullName { get; }
    }
}
