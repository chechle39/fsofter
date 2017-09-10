namespace CoffeeShop.Model.Abstract
{
    public interface IAuditable
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsDelete { get; set; }
    }
}
