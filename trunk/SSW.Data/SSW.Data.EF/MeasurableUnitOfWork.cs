using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.EF
{
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;

    using SSW.AnalyticsInterface.Core;

    public class MeasurableUnitOfWork : UnitOfWork
    {
        private readonly IMonitorFactory monitorFactory;

        public MeasurableUnitOfWork(IEnumerable<IDbContextManager> contextList, IMonitorFactory monitorFactory)
            : base(contextList)
        {
            this.monitorFactory = monitorFactory;
        }

        public override void SaveChanges()
        {
            try
            {
                foreach (var contextManager in this.contextList)
                {
                    if (contextManager.HasContext)
                    {
                        var databaseName = contextManager.Context.Database.Connection.Database;
                        var featureName = string.Format("Saving changes to {0}", databaseName);
                        var monitor = this.monitorFactory.Create();
                        monitor.TrackFeatureStart(featureName);

                        contextManager.Context.SaveChanges();

                        monitor.TrackFeatureStop(featureName);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx.Message, sqlEx);
                var ex = sqlEx.ToDataOperationException();
                throw ex;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException due)
            {
                logger.Error(due.Message, due);
                var ex = due.ToDataOperationException();
                throw ex;

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        logger.Error(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage), dbEx);
                    }
                }

                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                throw;
            }
        }

        public override async Task SaveChangesAsync()
        {
            try
            {
                foreach (var contextManager in this.contextList)
                {
                    if (contextManager.HasContext)
                    {
                        var databaseName = contextManager.Context.Database.Connection.Database;
                        var featureName = string.Format("Saving changes to {0}", databaseName);
                        var monitor = this.monitorFactory.Create();
                        monitor.TrackFeatureStart(featureName);

                        await contextManager.Context.SaveChangesAsync();

                        monitor.TrackFeatureStop(featureName);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx.Message, sqlEx);
                var ex = sqlEx.ToDataOperationException();
                throw ex;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException due)
            {
                logger.Error(due.Message, due);
                var ex = due.ToDataOperationException();
                throw ex;

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        logger.Error(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage), dbEx);
                    }
                }

                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
