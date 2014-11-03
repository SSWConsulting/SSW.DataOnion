namespace SSW.Data.EF
{
    using System.Data.Entity;

    public class DropCreateInitializer<T> : DropCreateDatabaseIfModelChanges<T> where T : DbContext
    {
        public DropCreateInitializer()
        {
        }

        protected override void Seed(T context)
        {   
        }
    }
}