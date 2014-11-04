using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.EF
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class DbContextFactory<T> : IDbContextFactory<T> where T : DbContext
    {
        private static bool hasSetInitializer;

        private readonly IDatabaseInitializer<T> dbInitializer;

        private readonly string connectionString;

        private readonly DbConnection connection = null;



        public DbContextFactory(IDatabaseInitializer<T> dbInitializer, string connectionString)
        {
            this.dbInitializer = dbInitializer;
            this.connectionString = connectionString;
        }

        public DbContextFactory(IDatabaseInitializer<T> dbInitializer, DbConnection connection)
        {
            this.dbInitializer = dbInitializer;
            this.connection = connection;
        }
        

        public virtual T Create()
        {
            if (!hasSetInitializer)
            {
                Database.SetInitializer(this.dbInitializer);

                hasSetInitializer = true;
            }

            Object[] args;
            if (this.connection != null) // are we using a dBconnection or a Connection string?
            {
                args = new Object[] { this.connection };
            }
            else
            {
                args = new Object[] { connectionString };
            }

            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
