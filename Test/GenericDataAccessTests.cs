using DataAccess;
using DataAccess.Models.PaymentModels;
using DataAccess.Services.IRepositories.IPaymentRepositories;
using DataAccess.Services.Repository.PaymentRepositories;
using FluentAssertions;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.Pipes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Common.Exceptions;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Test;

public class GenericDataAccessTests
{
    IHttpContextAccessor MockIHttpContextAccessor(HttpContext? context = null) 
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        context ??= new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new("EstablishmentId", "5")
            }, "TestAuthentication"))
        };
        mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        return mockHttpContextAccessor.Object;
    }
    IDbContextFactory<AppDbContext> MockIDbContextFactory(string dbName, HttpContext? httpContext = null)
    {
        var mockDbFactory = new Mock<IDbContextFactory<AppDbContext>>();

        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: dbName).Options;
        var httpContextAccessor = MockIHttpContextAccessor(httpContext);

        mockDbFactory.Setup(f => f.CreateDbContext()).Returns(() => new AppDbContext(options, httpContextAccessor));
        mockDbFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new AppDbContext(options, httpContextAccessor));

        return mockDbFactory.Object;
    }
    async Task<IDbContextFactory<AppDbContext>> PrepareTest(string? dbName = null, HttpContext? httpContext = null)
    {
        dbName ??= Guid.NewGuid().ToString(); 
        var dbContextFactory = MockIDbContextFactory(dbName, httpContext);
        
        var context = await dbContextFactory.CreateDbContextAsync();
        
        if(context.PaymentServices.Any()) await context.PaymentServices.IgnoreQueryFilters().ExecuteDeleteAsync();
        
        var paymentServices = new List<PaymentService>()
        {
            new()
            {
                Name = "PaymentService-1-Name",
                Provider = "PaymentService-1-Provider",
                Fee = 5,
                FeePercentage = 2.5,
            },
            new()
            {
                Name = "PaymentService-2-Name",
                Provider = "PaymentService-2-Provider",
                Fee = 0,
                FeePercentage = 4,
            },
            new()
            {
                Name = "PaymentService-3-Name",
                Provider = "PaymentService-3-Provider",
                Fee = 25,
                FeePercentage = 0.5,
            },
            new()
            {
                Name = "PaymentService-4-Name",
                Provider = "PaymentService-1-Provider",
                Fee = 5,
                FeePercentage = 1.5,
            },
            new()
            {
                Name = "PaymentService-5-Name",
                Provider = "PaymentService-2-Provider",
                Fee = 30,
                FeePercentage = 0,
            },
            new()
            {
                Name = "PaymentService-6-Name",
                Provider = "PaymentService-3-Provider",
                Fee = 15,
                FeePercentage = 3,
            },
            new()
            {
                Name = "PaymentService-7-Name",
                Provider = "PaymentService-3-Provider",
                Fee = 0,
                FeePercentage = 0,
            }
        };
        
        context.PaymentServices.AttachRange(paymentServices);
        await context.SaveChangesAsync();
        context.PaymentServices.RemoveRange(paymentServices.TakeLast(3));
        await context.SaveChangesAsync();

        return dbContextFactory;
    }

    protected static async Task GetAsyncHelper(IDbContextFactory<AppDbContext> dbContextFactory, int id, params Action<PaymentService?>[] asserts)
    {
        // Arrange
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);
        Result<PaymentService> queriedPaymentServiceResults = default;

        // Act
        queriedPaymentServiceResults = await paymentServiceRepository.GetByIdAsync(id);

        // Assert
        queriedPaymentServiceResults.Should().NotBeNull();
        _ = queriedPaymentServiceResults.Match(
                succ => { asserts.Iter(x => x(succ)); return Prelude.unit; },
                fail => {
                    if (fail is NotFoundException) asserts.Iter(x => x(null));
                    else Assert.Fail(fail.Message);
                    return Prelude.unit;
                }
            );
    }

    protected static async Task GetAllAsyncHelper(IDbContextFactory<AppDbContext> dbContextFactory, params Action<IEnumerable<PaymentService>?>[] asserts)
    {
        // Arrange
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);
        Result<IEnumerable<PaymentService>> queriedPaymentServiceResults = default;

        // Act
        queriedPaymentServiceResults = await paymentServiceRepository.GetAllAsync();

        // Assert
        queriedPaymentServiceResults.Should().NotBeNull();
        _ = queriedPaymentServiceResults.Match(
                succ => { asserts.Iter(x => x(succ)); return Prelude.unit; },
                fail => {
                    if (fail is NotFoundException) asserts.Iter(x => x(null));
                    else Assert.Fail(fail.Message);
                    return Prelude.unit;
                }
            );
    }

    protected static async Task FindAsyncHelper(IDbContextFactory<AppDbContext> dbContextFactory, Expression<Func<PaymentService, bool>> predicate,
        bool filterSoftDelete = true, params Action<IEnumerable<PaymentService>?>[] asserts)
    {
        // Arrange
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);
        Result<IEnumerable<PaymentService>> queriedPaymentServicesResults = default;

        // Act
        queriedPaymentServicesResults = await paymentServiceRepository.FindAsync(predicate, filterSoftDelete);

        // Assert
        queriedPaymentServicesResults.Should().NotBeNull();
        _ = queriedPaymentServicesResults.Match(
                succ => { asserts.Iter(x => x(succ)); return Prelude.unit; },
                fail => {
                    if (fail is NotFoundException) asserts.Iter(x => x(null));
                    else Assert.Fail(fail.Message);
                    return Prelude.unit;
                }
            );
    }

    protected static async Task FindFirstAsyncHelper(IDbContextFactory<AppDbContext> dbContextFactory, Expression<Func<PaymentService, bool>> predicate,
        params Action<PaymentService?>[] asserts)
    {
        // Arrange
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);
        Result<PaymentService> queriedPaymentServiceResults = default;

        // Act
        queriedPaymentServiceResults = await paymentServiceRepository.FindFirstAsync(predicate);

        // Assert
        queriedPaymentServiceResults.Should().NotBeNull();
        _ = queriedPaymentServiceResults.Match(
                succ => { asserts.Iter(x => x(succ)); return Prelude.unit; },
                fail => {
                    if (fail is NotFoundException) asserts.Iter(x => x(null));
                    else Assert.Fail(fail.Message);
                    return Prelude.unit;
                }
            );
    }

    [Fact]
    public async Task CreateAsyncTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<PaymentService> createdPaymentServiceResults = default;
        PaymentService? newPaymentService = new()
        {
            Name = "PaymentService-1-Name",
            Provider = "PaymentService-1-Provider",
            Fee = 5,
            FeePercentage = 3,
        };
        PaymentService? dbPaymentService = default;

        // Act
        createdPaymentServiceResults = await paymentServiceRepository.CreateAsync(newPaymentService);

        // Assert
        createdPaymentServiceResults.Should().NotBeNull();
        dbPaymentService = createdPaymentServiceResults.Match(
            succ => { succ.Should().NotBeNull(); succ.Name.Should().Be("PaymentService-1-Name"); return succ; },
            fail => { Assert.Fail(fail.Message); return null; });

        await GetAsyncHelper(dbContextFactory, dbPaymentService.Id, x => { x.Should().NotBeNull(); x.Should().BeEquivalentTo(dbPaymentService); });
    }

    [Fact]
    public async Task CreateRangeAsyncTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<IEnumerable<PaymentService>> createdPaymentServicesResults = default;
        List<PaymentService> newPaymentServices = new()
        {
            new()
            {
                Name = "PaymentService-2-Name",
                Provider = "PaymentService-2-Provider",
                Fee = 0,
                FeePercentage = 4,
            },
            new()
            {
                Name = "PaymentService-3-Name",
                Provider = "PaymentService-1-Provider",
                Fee = 25,
                FeePercentage = 0,
            }
        };
        IEnumerable<PaymentService>? dbPaymentServices = default;

        // Act
        createdPaymentServicesResults = await paymentServiceRepository.CreateRangeAsync(newPaymentServices);

        // Assert
        createdPaymentServicesResults.Should().NotBeNull();
        dbPaymentServices = createdPaymentServicesResults.Match(
            succ =>
            {
                succ.Should().NotBeNullOrEmpty();
                succ.Should().HaveCount(2);
                return succ;
            },
            fail => { Assert.Fail(fail.Message); return null; });

        await FindAsyncHelper(dbContextFactory, q => dbPaymentServices.Select(x => x.Id).Contains(q.Id), true,
            a =>
            {
                a.Should().NotBeNullOrEmpty();
                a.Should().BeEquivalentTo(dbPaymentServices);
            });
    }

    [Fact]
    public async Task UpdateAsyncTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<PaymentService> updatedPaymentServiceResults = default;
        PaymentService? dbPaymentService = (await paymentServiceRepository.GetByIdAsync(2))
            .Match(succ => succ, fail => { Assert.Fail(fail.Message); return null; });
        PaymentService? updatedPaymentService = default;

        // Act
        dbPaymentService.Name = "PaymentService-1-Name";
        dbPaymentService.Provider = "PaymentService-1-Provider";
        updatedPaymentServiceResults = await paymentServiceRepository.UpdateAsync(dbPaymentService);

        // Assert
        updatedPaymentServiceResults.Should().NotBeNull();
        updatedPaymentService = updatedPaymentServiceResults.Match(
            succ => { succ.Should().NotBeNull(); succ.Name.Should().Be("PaymentService-1-Name"); return succ; },
            fail => { Assert.Fail(fail.Message); return null; });

        await GetAsyncHelper(dbContextFactory, dbPaymentService.Id, x => { x.Should().NotBeNull(); x.Should().BeEquivalentTo(updatedPaymentService); });
    }

    [Fact]
    public async Task UpdateRangeAsyncTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<IEnumerable<PaymentService>> updatedPaymentServicesResults = default;
        IEnumerable<PaymentService>? dbPaymentServices = (await paymentServiceRepository.GetAllAsync())
            .Match(succ => succ, fail => { Assert.Fail(fail.Message); return null; });
        IEnumerable<PaymentService>? updatedPaymentServices = default;

        // Act
        int num = 1;
        foreach(PaymentService dbPaymentService in dbPaymentServices)
        {
            dbPaymentService.Name = $"PaymentService-{num}-Name";
            num++;
        }
        updatedPaymentServicesResults = await paymentServiceRepository.UpdateRangeAsync(dbPaymentServices);

        // Assert
        updatedPaymentServicesResults.Should().NotBeNull();
        updatedPaymentServices = updatedPaymentServicesResults.Match(
            succ => { succ.Should().NotBeNullOrEmpty(); succ.Should().BeEquivalentTo(dbPaymentServices); return succ; },
            fail => { Assert.Fail(fail.Message); return null; });

        await GetAllAsyncHelper(dbContextFactory, x => { x.Should().NotBeNull(); x.Should().BeEquivalentTo(updatedPaymentServices); });
    }

    [Fact]
    public async Task SoftDeleteAsyncTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<PaymentService> softDeletedPaymentServiceResults = default;
        PaymentService? dbPaymentService = (await paymentServiceRepository.GetByIdAsync(1))
            .Match(succ => succ, fail => { Assert.Fail(fail.Message); return null; });
        PaymentService? softDeletedPaymentService = default;

        // Act
        softDeletedPaymentServiceResults = await paymentServiceRepository.SoftDeleteAsync(dbPaymentService.Id);

        // Assert
        softDeletedPaymentServiceResults.Should().NotBeNull();
        softDeletedPaymentService = softDeletedPaymentServiceResults.Match(
            succ => { succ.Should().NotBeNull(); succ.SoftDelete.Should().BeLessThan(0); return succ; },
            fail => { Assert.Fail(fail.Message); return null; });

        await GetAsyncHelper(dbContextFactory, dbPaymentService.Id, x => { x.Should().BeNull(); });
    }

    [Fact]
    public async Task SoftDeleteRangeAsyncTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<IEnumerable<PaymentService>> softDeletedPaymentServicesResults = default;
        IEnumerable<PaymentService>? dbPaymentServices = (await paymentServiceRepository
            .FindAsync(x => x.Provider == "PaymentService-1-Provider"))
            .Match(succ => succ, fail => { Assert.Fail(fail.Message); return null; });
        IEnumerable<PaymentService>? softDeletedPaymentServices = default;

        // Act
        softDeletedPaymentServicesResults = await paymentServiceRepository
            .SoftDeleteRangeAsync(dbPaymentServices.Select(x => x.Id));

        // Assert
        softDeletedPaymentServicesResults.Should().NotBeNull();
        softDeletedPaymentServices = softDeletedPaymentServicesResults.Match(
            succ => { succ.Should().NotBeNull(); succ.Iter(x => x.SoftDelete.Should().BeLessThan(0)); return succ; },
            fail => { Assert.Fail(fail.Message); return null; });

        await FindAsyncHelper(dbContextFactory, x => x.SoftDelete < 0, false, x => x.Select(x => x.SoftDelete.Should().BeLessThan(0)));
        await GetAllAsyncHelper(dbContextFactory, x => x.Select(x => x.Provider).Should().NotContainMatch("PaymentService-1-Provider"));
    }

    [Fact]
    public async Task RecoverAsyncTest()
    {
        // Arrange
        Result<PaymentService> recoveredPaymentServiceResults = default;
        PaymentService? dbSoftDeletedPaymentService = default;
        PaymentService? recoveredPaymentService = default;

        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        using (var context = await dbContextFactory.CreateDbContextAsync())
        {
            context.FilterSoftDelete = false;
            dbSoftDeletedPaymentService = (await paymentServiceRepository
                .GetByIdAsync(context.PaymentServices.Last().Id, false))
                .Match(succ => succ, fail => { Assert.Fail(fail.Message); return null; });
        };

        // Act
        recoveredPaymentServiceResults = await paymentServiceRepository.RecoverAsync(dbSoftDeletedPaymentService.Id);

        // Assert
        recoveredPaymentServiceResults.Should().NotBeNull();
        recoveredPaymentService = recoveredPaymentServiceResults.Match(
            succ => { succ.Should().NotBeNull(); succ.SoftDelete.Should().BeGreaterThanOrEqualTo(0); return succ; },
            fail => { Assert.Fail(fail.Message); return null; });

        await GetAsyncHelper(dbContextFactory, dbSoftDeletedPaymentService.Id, x => { x.Should().NotBeNull().And.BeEquivalentTo(recoveredPaymentService); });
    }

    [Fact]
    public async Task RecoverRangeAsyncTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<IEnumerable<PaymentService>> recoveredPaymentServicesResults = default;
        IEnumerable<PaymentService>? dbSoftDeletedPaymentServices = 
            (await paymentServiceRepository.FindAsync(x => x.SoftDelete < 0, false))
            .Match(succ => succ, fail => { Assert.Fail(fail.Message); return null; });
        IEnumerable<PaymentService>? recoveredPaymentServices = default;

        // Act
        recoveredPaymentServicesResults = await paymentServiceRepository.RecoverRangeAsync(x => x.SoftDelete < 0);

        // Assert
        recoveredPaymentServicesResults.Should().NotBeNull();
        recoveredPaymentServices = recoveredPaymentServicesResults.Match(
            succ =>
            {
                succ.Should().NotBeNull();
                succ.Iter(x => x.SoftDelete.Should().BeGreaterThanOrEqualTo(0));
                return succ;
            },
            fail => { Assert.Fail(fail.Message); return null; });

        await FindAsyncHelper(dbContextFactory,
            x => dbSoftDeletedPaymentServices.Select(x => x.Id).Contains(x.Id), true,
            x => { x.Should().NotBeNull().And.BeEquivalentTo(recoveredPaymentServices); });
    }

    [Fact]
    public async Task RecoverRangeAsyncWithIdsTest()
    {
        // Arrange
        var dbContextFactory = await PrepareTest();
        IPaymentServiceRepository paymentServiceRepository = new PaymentServiceRepository(dbContextFactory);

        Result<IEnumerable<PaymentService>> recoveredPaymentServicesResults = default;
        IEnumerable<PaymentService>? dbSoftDeletedPaymentServices =
            (await paymentServiceRepository.FindAsync(x => x.SoftDelete < 0, false))
            .Match(succ => succ, fail => { Assert.Fail(fail.Message); return null; });
        IEnumerable<PaymentService>? recoveredPaymentServices = default;

        // Act
        recoveredPaymentServicesResults = await paymentServiceRepository
            .RecoverRangeAsync(dbSoftDeletedPaymentServices.Select(x => x.Id));

        // Assert
        recoveredPaymentServicesResults.Should().NotBeNull();
        recoveredPaymentServices = recoveredPaymentServicesResults.Match(
            succ =>
            {
                succ.Should().NotBeNull();
                succ.Iter(x => x.SoftDelete.Should().BeGreaterThanOrEqualTo(0));
                return succ;
            },
            fail => { Assert.Fail(fail.Message); return null; });

        await FindAsyncHelper(dbContextFactory,
            x => dbSoftDeletedPaymentServices.Select(x => x.Id).Contains(x.Id), true,
            x => { x.Should().NotBeNull().And.BeEquivalentTo(recoveredPaymentServices); });
    }
}
