using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class DbContextFactory<T> : IDbContextFactory<T> where T : DbContext
    {
        private static bool hasSetInitializer;

        private readonly IDatabaseInitializer<T> dbInitializer;

        private readonly string connectionString;



        public DbContextFactory(IDatabaseInitializer<T> dbInitializer, string connectionString)
        {
            this.dbInitializer = dbInitializer;
            this.connectionString = connectionString;
        }

        public T Create()
        {
            if (!hasSetInitializer)
            {
                Database.SetInitializer(this.dbInitializer);

                hasSetInitializer = true;
            }

            var args = new Object[] { connectionString };
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
