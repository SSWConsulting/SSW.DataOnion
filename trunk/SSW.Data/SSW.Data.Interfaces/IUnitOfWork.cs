namespace SSW.Data.Interfaces
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
