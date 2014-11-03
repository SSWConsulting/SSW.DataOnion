namespace SSW.Data.Tests.Integration
{
    using System.Data.Entity;

    public class DropCreateInitializer : DropCreateDatabaseIfModelChanges<YourDbContext>
    {
        public DropCreateInitializer()
        {
        }

        protected override void Seed(YourDbContext context)
        {   
        }
    }
}