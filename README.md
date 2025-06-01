# 單元測試 Stub、Mock、Fake 實戰範例（C#，NUnit + NSubstitute）

## 目錄

1. [第一個範例：Stub（查詢用戶名稱）](#stub)
2. [第二個範例：Stub + Mock（用戶註冊）](#stubmock)
3. [第三個範例：Stub + Mock + Fake（查詢產品庫存）](#stubmockfake)

---

<a name="stub"></a>

## 1. 第一個範例：Stub

### 功能：查詢用戶名稱

**情境說明**
Server 只需從 repository 取出資料，不涉及副作用與驗證。

```csharp
// 介面設計
public interface IUserRepository
{
    string GetUserName(int userId);
}

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public string GetUserName(int userId)
    {
        return _repo.GetUserName(userId);
    }
}

// 單元測試（Stub）
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class UserServiceTests
{
    [Test]
    public void GetUserName_ReturnsCorrectName()
    {
        // Arrange：建立 stub（固定回傳值的假物件）
        var stubRepo = Substitute.For<IUserRepository>();
        stubRepo.GetUserName(1).Returns("Charles");

        var service = new UserService(stubRepo);

        // Act
        var result = service.GetUserName(1);

        // Assert
        Assert.AreEqual("Charles", result);
    }
}
```

> **說明**
>
> * 這裡的 stub 僅提供預期的回傳值，不會檢查是否有調用，單純「餵資料」給 SUT（System Under Test）。

---

<a name="stubmock"></a>

## 2. 第二個範例：Stub + Mock

### 功能：用戶註冊

**情境說明**
要先檢查用戶是否已存在，並且要驗證 repository 方法有被呼叫（副作用）。

```csharp
public interface IUserRepository
{
    bool Exists(string userName);
    void AddUser(string userName);
}

public class RegisterService
{
    private readonly IUserRepository _repo;

    public RegisterService(IUserRepository repo)
    {
        _repo = repo;
    }

    public bool Register(string userName)
    {
        if (_repo.Exists(userName))
            return false;

        _repo.AddUser(userName);
        return true;
    }
}

// 單元測試（Stub + Mock）
[TestFixture]
public class RegisterServiceTests
{
    [Test]
    public void Register_WhenUserDoesNotExist_AddsUser()
    {
        var repo = Substitute.For<IUserRepository>();

        // Stub: 預先定義 repository 的回傳值
        repo.Exists("Alice").Returns(false);

        var service = new RegisterService(repo);

        // Act
        var result = service.Register("Alice");

        // Assert: Mock 的觀念（驗證方法是否被呼叫）
        repo.Received(1).AddUser("Alice");
        Assert.IsTrue(result);
    }

    [Test]
    public void Register_WhenUserExists_DoesNotAddUser()
    {
        var repo = Substitute.For<IUserRepository>();
        repo.Exists("Alice").Returns(true);

        var service = new RegisterService(repo);

        var result = service.Register("Alice");

        repo.DidNotReceive().AddUser(Arg.Any<string>());
        Assert.IsFalse(result);
    }
}
```

> **Stub 與 Mock 差異說明**
>
> * **Stub**：僅定義特定方法的回傳值，不關心是否真的被呼叫。
> * **Mock**：除了定義回傳值，也會驗證是否有呼叫、呼叫次數等「互動」細節，確保行為正確。

---

<a name="stubmockfake"></a>

## 3. 第三個範例：Stub + Mock + Fake

### 功能：查詢產品庫存

**情境說明**
需要一個有狀態的假 repository（in-memory fake），不僅 stub/mock，而且可以存放假資料。

```csharp
public interface IProductRepository
{
    int GetStock(string productId);
    void SetStock(string productId, int count);
}

public class ProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public bool IsInStock(string productId)
    {
        return _repo.GetStock(productId) > 0;
    }
}

// Fake 實作
public class FakeProductRepository : IProductRepository
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

// 單元測試（Stub + Mock + Fake）
[TestFixture]
public class ProductServiceTests
{
    [Test]
    public void IsInStock_WithStock_ReturnsTrue()
    {
        // 使用 fake repository
        var fakeRepo = new FakeProductRepository();
        fakeRepo.SetStock("P001", 5);

        var service = new ProductService(fakeRepo);

        Assert.IsTrue(service.IsInStock("P001"));
    }

    [Test]
    public void IsInStock_NoStock_ReturnsFalse()
    {
        var fakeRepo = new FakeProductRepository();
        fakeRepo.SetStock("P002", 0);

        var service = new ProductService(fakeRepo);

        Assert.IsFalse(service.IsInStock("P002"));
    }
}
```

---

<a name="diff"></a>

## 三者差異與前瞻性建議

| 名稱   | 定義           | 適用場景            | 重點                    |
| ---- | ------------ | --------------- | --------------------- |
| Stub | 提供預設回傳值      | 查詢資料，不驗證互動      | 不驗證行為，只餵資料            |
| Mock | 驗證是否有呼叫/互動   | 需確認副作用或重要行為時    | 驗證互動（如呼叫次數）           |
| Fake | 有簡單邏輯，具備內部狀態 | 整合測試、多次互動需維持狀態時 | 類似 in-memory 資料庫，方便測試 |

---
