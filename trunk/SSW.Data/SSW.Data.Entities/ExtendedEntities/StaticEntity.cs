using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Entities.ExtendedEntities
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public abstract class StaticEntity : BaseEntity
    {
        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public static IEnumerable<T> GetAll<T>() where T : StaticEntity
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Where(f => f.FieldType == typeof(T)).Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FromId<T>(int value) where T : StaticEntity
        {
            var matchingItem = Parse<T>(item => item.Id == value);
            return matchingItem;
        }

        public static T FromName<T>(string name) where T : StaticEntity
        {
            var matchingItem = Parse<T>(item => item.Name == name);
            return matchingItem;
        }

        public static T FromDescription<T>(string description) where T : StaticEntity
        {
            var matchingItem = Parse<T>(item => item.Description == description);
            return matchingItem;
        }

        private static T Parse<T>(Func<T, bool> predicate) where T : StaticEntity
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            return matchingItem;
        }
    }
}
