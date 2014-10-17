namespace $rootnamespace$
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class YourDbContextFactory : IDbContextFactory<YourDbContext>
    {
        private bool hasSetInitializer;

        private IDatabaseInitializer<YourDbContext> dbInitializer;

        public YourDbContextFactory(IDatabaseInitializer<YourDbContext> dbInitializer)
        {
            this.dbInitializer = dbInitializer;
        }

        public YourDbContext Create()
        {
            if (!this.hasSetInitializer)
            {
                Database.SetInitializer(this.dbInitializer);

                this.hasSetInitializer = true;
            }

            return new YourDbContext();
        }
    }
}
