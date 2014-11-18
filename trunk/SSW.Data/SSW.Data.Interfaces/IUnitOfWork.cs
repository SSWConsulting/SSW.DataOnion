namespace SSW.Data.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
