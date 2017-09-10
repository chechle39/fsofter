namespace CoffeeShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}