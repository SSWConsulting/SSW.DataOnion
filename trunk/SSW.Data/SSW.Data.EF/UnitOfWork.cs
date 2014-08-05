namespace SSW.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;

    using SSW.Common;
    using SSW.Common.Exceptions;
    using SSW.Data.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {
        IEnumerable<IDbContextManager> contextList;

        private readonly ILogger logger;

        private bool disposed;

        public UnitOfWork(IEnumerable<IDbContextManager> contextList, ILogger logger)
        {
            this.contextList = contextList;
            this.logger = logger;
        }

        public void SaveChanges()
        {
            try
            {
                foreach (var contextManager in this.contextList)
                {
                    if (contextManager.HasContext)
                    {
                        contextManager.Context.SaveChanges();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                this.logger.Error(sqlEx.Message, sqlEx);
                var ex = sqlEx.ToDataOperationException();
                throw ex;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException due)
            {
                this.logger.Error(due.Message, due);
                var ex = due.ToDataOperationException();
                throw ex;

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        this.logger.Error(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage), dbEx);
                    }
                }

                throw;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message, ex);
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    foreach (var contextManager in this.contextList)
                    {
                        if (contextManager != null)
                        {
                            contextManager.Dispose();
                        }
                    }
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public static class SQLExceptionExtensions
    {

        public static DataOperationException ToDataOperationException(this System.Data.Entity.Infrastructure.DbUpdateException dbUpdateException)
        {
            DataOperationException result = null;

            Exception innerMostException = dbUpdateException.InnerMostException();
            var sqlException = innerMostException as SqlException; //if (innerMostException is SqlException) if (innerMostException.GetType().IsAssignableFrom(typeof(SqlException)))
            if (sqlException != null)
            {
                result = sqlException.ToDataOperationException();
            }
            else
            {
                result = new DataOperationException("Unknown Data Error", dbUpdateException);
            }
            return result;

        }

        public static DataOperationException ToDataOperationException(this SqlException ex)
        {
            DataOperationException dataException = null;
            if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
            {
                switch (ex.Errors[0].Number)
                {
                    case 547: // Foreign Key violation
                        dataException = new DataOperationException("This record is in use.");
                        break;
                    case 2601: // Primary key violation
                        dataException = new DataOperationException("Primary key violation.");
                        break;
                    default:
                        dataException = new DataOperationException("Unknown data exception.");
                        break;
                }
            }
            return dataException;
        }

    }
}
