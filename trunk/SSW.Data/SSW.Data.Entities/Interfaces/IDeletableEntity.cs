namespace SSW.Data.Entities
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}
