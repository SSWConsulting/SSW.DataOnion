namespace SSW.Data1.Data
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    using SSW.Data.Entities;

    /// <summary>
    /// Database context
    /// </summary>
    public partial class YourDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YourDbContext"/> class.
        /// </summary>
        public YourDbContext()
            : base("name=YourConnectionStringName")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YourDbContext" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public YourDbContext(DbConnection connection)
            : base(connection, true)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YourDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        public YourDbContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Saves pending changes. Populates date created and data modified fields
        /// </summary>
        /// <returns>
        /// Number of records saved
        /// </returns>
        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = HttpContext.Current != null && HttpContext.Current.User != null
                ? HttpContext.Current.User.Identity.Name
                : "system";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.Now;
                    ((BaseEntity)entity.Entity).CreatedBy = string.IsNullOrEmpty(((BaseEntity)entity.Entity).CreatedBy) ? currentUsername : ((BaseEntity)entity.Entity).CreatedBy;
                }

                ((BaseEntity)entity.Entity).LastModifiedDate = DateTime.Now;
                ((BaseEntity)entity.Entity).LastModifiedBy = currentUsername;
            }

            int result;
            try
            {
                result = base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ((IObjectContextAdapter)this).ObjectContext.Refresh(RefreshMode.ClientWins, ex.Entries.Select(entry => entry.Entity));
                result = base.SaveChanges();
            }

            return result;
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        /// but
        /// before the model has been locked down and used to initialize the context.  The
        /// default
        /// implementation of this method does nothing, but it can be overridden in a derived
        /// class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context
        /// being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived
        /// context
        /// is created.  The model for that context is then cached and is for all further
        /// instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuiller, but note that this can seriously degrade
        /// performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
