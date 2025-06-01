using WebApplication.Repository.Interface;

namespace WebApplication.Tests;

public class FakeProductRepository: IProductRepository
{
    private readonly Dictionary<string, int> _stocks = new Dictionary<string, int>();

    public int GetStock(string productId)
    {
        return _stocks.TryGetValue(productId, out var count) ? count : 0;
    }

    public void SetStock(string productId, int count)
    {
        _stocks[productId] = count;
    }
}