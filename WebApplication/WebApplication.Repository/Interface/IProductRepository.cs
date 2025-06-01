namespace WebApplication.Repository.Interface;

public interface IProductRepository
{
    int GetStock(string productId);
    void SetStock(string productId, int count);
}