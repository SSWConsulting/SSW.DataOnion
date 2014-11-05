namespace SSW.Data1.Data
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