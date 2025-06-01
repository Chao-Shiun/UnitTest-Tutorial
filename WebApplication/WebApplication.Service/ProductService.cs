using WebApplication.Repository.Interface;

namespace WebApplication.Service;

public class ProductService(IProductRepository repo)
{
    public bool IsInStock(string productId)
    {
        return repo.GetStock(productId) > 0;
    }
}