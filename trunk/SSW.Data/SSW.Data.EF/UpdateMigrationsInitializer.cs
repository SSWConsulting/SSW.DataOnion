// Type: System.Data.Entity.MigrateDatabaseToLatestVersion`2
// Assembly: EntityFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: EE77DA9B-E5EB-48FD-8745-5CCD8AEACCEC
// Assembly location: C:\DataBrendanRichards\ProjectsTFS\CatholicEducation\Sparrow\trunk\BCE.Sparrow\packages\EntityFramework.5.0.0\lib\net45\EntityFramework.dll

using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

namespace SSW.Framework.Data.EF
{
  /// <summary>
  /// An implementation of <see cref="T:System.Data.Entity.IDatabaseInitializer`1"/> that will use Code First Migrations
  ///             to update the database to the latest version.
  /// This replaces the initializer provided by EF so that we can inject the configuration via constructor
  /// 
  /// </summary>
  public class UpdateMigrationsInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
  {

      private readonly DbMigrationsConfiguration<TContext> _config;


      /// <summary>
      /// Initializes a new instance of the MigrateDatabaseToLatestVersion class.
      /// 
      /// </summary>
      public UpdateMigrationsInitializer(DbMigrationsConfiguration<TContext> config)
      {
        this._config = config;
      }

   

    /// <inheritdoc/>
    public void InitializeDatabase(TContext context)
    {
      //RuntimeFailureMethods.Requires((object) context != null, (string) null, "context != null");
      ((MigratorBase) new DbMigrator(this._config)).Update();
    }

     
  }
}
