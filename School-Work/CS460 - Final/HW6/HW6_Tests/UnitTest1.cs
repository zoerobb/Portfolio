using HW6.Models;
using HW6.Services;
using HW6.DAL.Abstract;
using HW6.DAL.Concrete;
using Microsoft.EntityFrameworkCore;
using Moq;
using HW6.ExtensionMethods;

namespace HW6_Tests;

public class OrderRepository_Tests
{
    private Mock<CoffeeShopDbContext> _mockContext;
    private Mock<DbSet<Order>> _mockOrder;
    private List<Order> _order;
    
    private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
        return mockSet;
    }

    [SetUp]
    public void Setup()
    {
        _order = new List<Order>
        {
            new Order
            {
                Id = 1,
                Name = "Benjamin",
                DeliveryId = 1,
                StoreId = 1,
                TotalPrice = 49.38M,
                Complete = true,
                TimeArrived = new DateTime(2023, 12, 01, 18, 11, 45, 663)
            },
            new Order
            {
                Id = 2018,
                Name = "Nicole",
                DeliveryId = 2,
                StoreId = 1,
                TotalPrice = 37.91M,
                Complete = false,
                TimeArrived = new DateTime(2023, 12, 03, 13, 57, 49, 863)
            },
            new Order
            {
                Id = 2024,
                Name = "Mary",
                DeliveryId = 3,
                StoreId = 1,
                TotalPrice = 35.43M,
                Complete = false,
                TimeArrived = new DateTime(2023, 12, 03, 13, 58, 06, 28)
            },
        };

        _mockContext = new Mock<CoffeeShopDbContext>();
        _mockOrder = GetMockDbSet(_order.AsQueryable());
        _mockContext.Setup(ctx => ctx.Orders).Returns(_mockOrder.Object);
        _mockContext.Setup(ctx => ctx.Set<Order>()).Returns(_mockOrder.Object);
    }

    [Test]
    public void CheckStatus_GivenId_ReturnsCompletionStatus()
    {
        //Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        //Act
        var result = repo.CheckStatus(1);
        var secondResult = repo.CheckStatus(2018);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(true));
            Assert.That(secondResult, Is.EqualTo(false));
        });
    }

    [Test]
    public void CheckStatus_GivenThatDoesNotExist_ReturnsException()
    {
        //Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        //Act & Assert
        Assert.Throws<Exception>(() => repo.CheckStatus(5));
    }

    [Test]
    public void GetAllAscending_Returns_ListOfAllOrdersInOrder()
    {
        //Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        //Act
        var result = repo.GetAllAscending();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Id, Is.EqualTo(2018));
            Assert.That(result[1].Id, Is.EqualTo(2024));
            Assert.That(result.Count, Is.EqualTo(2));
        });
    }
}

public class MenuItemRepository_Tests
{
    private Mock<CoffeeShopDbContext> _mockContext;
    private Mock<DbSet<MenuItem>> _mockMenuItems;
    private List<MenuItem> _menuItems;
    
    private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
        return mockSet;
    }

    [SetUp]
    public void Setup()
    {
        _menuItems = new List<MenuItem>
        {
            new MenuItem
            {
                Id = 1,
                Name = "Caramel Macchiato",
                Description = "Rich espresso with caramel and steamed milk",
                Price = 4.99M,
            },
            new MenuItem
            {
                Id = 2,
                Name = "Glazed Doughnut",
                Description = " Classic ring-shaped doughnut with sweet glaze",
                Price = 2.49M,
            },
        };

        _mockContext = new Mock<CoffeeShopDbContext>();
        _mockMenuItems = GetMockDbSet(_menuItems.AsQueryable());
        _mockContext.Setup(ctx => ctx.MenuItems).Returns(_mockMenuItems.Object);
        _mockContext.Setup(ctx => ctx.Set<MenuItem>()).Returns(_mockMenuItems.Object);
    }

    [Test]
    public void GetPrice_GivenAnId_Returns_PriceOfAnItem()
    {
        //Arrange
        IMenuItemRepository repo = new MenuItemRepository(_mockContext.Object);

        //Act
        var result = repo.GetPrice(1);
        var secondResult = repo.GetPrice(2);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(4.99));
            Assert.That(secondResult, Is.EqualTo(2.49));
        });
    }

    [Test]
    public void GetPrice_GivenIdThatDoesNotExist_Returns_Exception()
    {
        //Arrange
        IMenuItemRepository repo = new MenuItemRepository(_mockContext.Object);

        //Act & Assert
        Assert.Throws<Exception>(() => repo.GetPrice(100));
    }
}

public class OrderedItemRepository_Tests
{
    private Mock<CoffeeShopDbContext> _mockContext;
    private Mock<DbSet<OrderedItem>> _mockOrderedItem;
    private List<OrderedItem> _orderedItem;
    
    private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
        return mockSet;
    }

    [SetUp]
    public void Setup()
    {
        _orderedItem = new List<OrderedItem>
        {
            new OrderedItem
            {
                Id = 1,
                MenuItemId = 1,
                OrderId = 1,
                Quantity = 2,
                Complete = true
            },
            new OrderedItem
            {
                Id = 2,
                MenuItemId = 6,
                OrderId = 1,
                Quantity = 6,
                Complete = false
            },
            new OrderedItem
            {
                Id = 8,
                MenuItemId = 32,
                OrderId = 3,
                Quantity = 1,
                Complete = false
            }
        };

        _mockContext = new Mock<CoffeeShopDbContext>();
        _mockOrderedItem = GetMockDbSet(_orderedItem.AsQueryable());
        _mockContext.Setup(ctx => ctx.OrderedItems).Returns(_mockOrderedItem.Object);
        _mockContext.Setup(ctx => ctx.Set<OrderedItem>()).Returns(_mockOrderedItem.Object);
    }

    [Test]
    public void CheckStatus_GivenId_ReturnsCompletionStatus()
    {
        //Arrange
        IOrderedItemRepository repo = new OrderedItemRepository(_mockContext.Object);

        //Act
        var result = repo.CheckStatus(1);
        var secondResult = repo.CheckStatus(2);
        var thirdResult = repo.CheckStatus(8);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(true));
            Assert.That(secondResult, Is.EqualTo(false));
            Assert.That(thirdResult, Is.EqualTo(false));
        });
    }

    [Test]
    public void CheckStatus_GivenThatDoesNotExist_ReturnsException()
    {
        //Arrange
        IOrderedItemRepository repo = new OrderedItemRepository(_mockContext.Object);

        //Act & Assert
        Assert.Throws<Exception>(() => repo.CheckStatus(32));
    }
}