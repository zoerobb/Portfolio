using HW6.Models;
using HW6.Services;
using HW6.DAL.Abstract;
using HW6.DAL.Concrete;
using Microsoft.EntityFrameworkCore;
using Moq;
using HW6.ExtensionMethods;

namespace HW6_Tests;

public class StationRepository_Tests
{
    private Mock<CoffeeShopDbContext> _mockContext;
    private Mock<DbSet<Station>> _mockStation;
    private List<Station> _station;
    
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
        _station = new List<Station>
        {
            new Station
            {
                Id = 1,
                Name = "Espresso Station",
            },
            new Station
            {
                Id = 2,
                Name = "Pastry Station",
            },
        };

        _mockContext = new Mock<CoffeeShopDbContext>();
        _mockStation = GetMockDbSet(_station.AsQueryable());
        _mockContext.Setup(ctx => ctx.Stations).Returns(_mockStation.Object);
        _mockContext.Setup(ctx => ctx.Set<Station>()).Returns(_mockStation.Object);
    }

    [Test]
    public void GetStationName_GivenId_Returns_StationNameAsAString()
    {
        //Arrange
        IStationRepository repo = new StationRepository(_mockContext.Object);

        //Act
        var result = repo.GetStationName(1);
        var secondResult = repo.GetStationName(2);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo("Espresso Station"));
            Assert.That(secondResult, Is.EqualTo("Pastry Station"));
        });
    }

    [Test]
    public void GetStationName_GivenIdThatDoesNotExist_ReturnsException()
    {
        //Arrange
        IStationRepository repo = new StationRepository(_mockContext.Object);

        //Act & Assert
        Assert.Throws<Exception>(() => repo.GetStationName(100));
    }
}