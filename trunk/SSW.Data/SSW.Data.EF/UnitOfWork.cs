namespace SSW.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;

    using SSW.Data.Interfaces;
    using SSW.Common;
    using SSW.Common.Exceptions;

    public class UnitOfWork : IUnitOfWork
    {
        IEnumerable<IDbContextManager> contextList;

        private readonly ILogger logger;

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

        public void Dispose()
        {
            foreach (var contextManager in this.contextList)
            {
                contextManager.Dispose();
            }
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
