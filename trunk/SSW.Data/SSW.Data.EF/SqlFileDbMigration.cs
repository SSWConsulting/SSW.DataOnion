namespace SSW.Data.EF
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.IO;

    public class SqlFileDbMigration : DbMigration
    {
        public SqlFileDbMigration()
        {   
        }

        private string GetUpSql()
        {
            return this.ReadEmbeddedFile(this.GetUpFileName());
        }

        private string GetDownSql()
        {
            return this.ReadEmbeddedFile(this.GetDownFileName());
        }

        private string GetUpFileName()
        {
            return this.GetType().FullName + ".sql";
        }

        private string GetDownFileName()
        {
            return this.GetType().FullName + "_down.sql";
        }

        private string ReadEmbeddedFile(string name)
        {
            string result;
            using (var stream = this.GetType().Assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        public override void Up()
        {
            string sql = this.GetUpSql();
            if (sql == null)
            {
                throw new MigrationsException("Failed to find SQL for up migration. SqlFileMigration expects an embedded resource named " + this.GetUpFileName());
            }

            this.Sql(sql);
        }

        public override void Down()
        {
            string sql = this.GetDownSql();
            if (sql == null)
            {
                throw new MigrationsException("Failed to find SQL for down migration. SqlFileMigration expects an embedded resource named " + this.GetDownFileName());
            }

            this.Sql(sql);
        }
    }
}
