using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Tests.DomainModel.Entities
{
    public class TestEntity1
    {

        [Key]
        public int Id { get; set; }


        public string Name { get; set; }
    }
}
