namespace Northwind.FruitStore.Interfaces.Business
{
    public interface INotificationProvider
    {
        bool Send(string to, string subject, string message);
    }
}