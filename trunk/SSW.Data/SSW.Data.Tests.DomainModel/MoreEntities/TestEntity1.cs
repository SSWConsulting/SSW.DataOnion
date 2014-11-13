using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Tests.DomainModel.MoreEntities
{
    public class TestEntity2
    {

        [Key]
        public int Id { get; set; }


        public string Name { get; set; }
    }
}
