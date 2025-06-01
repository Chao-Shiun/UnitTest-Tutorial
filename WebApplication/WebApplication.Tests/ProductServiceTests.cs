using WebApplication.Service;

namespace WebApplication.Tests;

public class ProductServiceTests
{
    [Test]
    public void IsInStock_WithStock_ReturnsTrue()
    {
        // 使用 fake repository
        var fakeRepo = new FakeProductRepository();
        fakeRepo.SetStock("P001", 5);

        var service = new ProductService(fakeRepo);

        Assert.That(true,Is.EqualTo(service.IsInStock("P001")));
    }

    [Test]
    public void IsInStock_NoStock_ReturnsFalse()
    {
        var fakeRepo = new FakeProductRepository();
        fakeRepo.SetStock("P002", 0);

        var service = new ProductService(fakeRepo);

        Assert.That(false,Is.EqualTo(service.IsInStock("P002")));
    }
}