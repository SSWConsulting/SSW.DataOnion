namespace SSW.Data1.DomainModel.Entities
{
    using SSW.Data.Entities;

    public class Entity2 : BaseEntity
    {
        public Entity1 Entity1 { get; set; }

        public int Prop1 { get; set; }
    }
}
