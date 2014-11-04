using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Tests.Integration
{
    public class EffortConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            Type _ = typeof(SqlProviderServices); // poke class to oad into app domain

            return Effort.DbConnectionFactory.CreateTransient();
        }
    }
}
