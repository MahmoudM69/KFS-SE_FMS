using Common.Attributes;
using Common.Enums;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Services.IRepositories.ISharedRepositories;
using DataAccess.Services.IRepositories.IPaymentRepositories;
using DataAccess.Models.OrderModels;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Services.IRepositories.IUserRepositories;
using DataAccess.Models.ProductModels;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Models.PaymentModels;
using DataAccess.Services.IRepositories.IFinancialAidRepositories;
using DataAccess.Services.IRepositories.IOrderRepositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace DataAccess.Services;

[Service(nameof(IDBInitializer))]
public class DBInitializer : IDBInitializer
{
    private readonly IEstablishmentRepository _establishmentRepository;
    private readonly IEstablishmentImageRepository _establishmentImageRepository;
    private readonly IEstablishmentTypeRepository _establishmentTypeRepository;
    private readonly IFinancialAidRepository _financialAidRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentServiceRepository _paymentServiceRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductImageRepository _productImageRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IEstablishment_ProductRepository _establishment_ProductRepository;
    private readonly IProductType_FinancialAidRepository _productType_FinancialAidRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICustomerRepository _customerRepository;

    public DBInitializer(IEstablishmentRepository establishmentRepository, IEstablishmentImageRepository establishmentImageRepository,
        IEstablishmentTypeRepository establishmentTypeRepository, IFinancialAidRepository financialAidRepository, IOrderRepository orderRepository,
        IPaymentRepository paymentRepository, IPaymentServiceRepository paymentServiceRepository, IProductRepository productRepository,
        IProductImageRepository productImageRepository, IProductTypeRepository productTypeRepository,
        IEstablishment_ProductRepository establishment_ProductRepository, IProductType_FinancialAidRepository productType_FinancialAidRepository,
        IEmployeeRepository employeeRepository, ICustomerRepository customerRepository)
    {
        _establishmentRepository = establishmentRepository;
        _establishmentImageRepository = establishmentImageRepository;
        _establishmentTypeRepository = establishmentTypeRepository;
        _financialAidRepository = financialAidRepository;
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
        _paymentServiceRepository = paymentServiceRepository;
        _productRepository = productRepository;
        _productImageRepository = productImageRepository;
        _productTypeRepository = productTypeRepository;
        _establishment_ProductRepository = establishment_ProductRepository;
        _productType_FinancialAidRepository = productType_FinancialAidRepository;
        _employeeRepository = employeeRepository;
        _customerRepository = customerRepository;
    }
    public void Initialize()
    {
        //if(context.Database.GetPendingMigrations().Count() > 0)
        //{
        //    context.Database.Migrate();
        //}


        var dbCustomers = _customerRepository.GetAllAsync().GetAwaiter().GetResult();
        var dbEmployees = _employeeRepository.GetAllAsync().GetAwaiter().GetResult();
        var dbEstablishments = _establishmentRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbEstablishmentTypes = _establishmentTypeRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbEstablishmentImages = _establishmentImageRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbFinancialAids = _financialAidRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbOrders = _orderRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbPayments = _paymentRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbPaymentServices = _paymentServiceRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProducts = _productRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProductTypes = _productTypeRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProductImages = _establishmentImageRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbEstablishment_Products = _establishment_ProductRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProduct_Financials = _productType_FinancialAidRepository.GetAllAsync().GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var establishmentType = (dbEstablishmentTypes is not null && dbEstablishmentTypes.Any()) ? dbEstablishmentTypes.First() :
            _establishmentTypeRepository.CreateAsync(new()
            {
                Type = "Establishment_Type_Name_Test_1",
                Description = "Establishment_Type_Description_Test_1",
            }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var establishmentTypes = (dbEstablishmentTypes is not null && dbEstablishmentTypes.Any()) ? dbEstablishmentTypes.Skip(1) : 
            _establishmentTypeRepository.CreateRangeAsync(new List<EstablishmentType>() {
            new()
            {
                Type = "Establishment_Type_Name_Test_2",
                Description = "Establishment_Type_Description_Test_2",
            },
            new()
            {
                Type = "Establishment_Type_Name_Test_3",
                Description = "Establishment_Type_Description_Test_3",
            }
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var establishment = _establishmentRepository.CreateAsync(new Establishment
        {
            Name = "Establishment_Name_Test_1",
            Description = "Establishmen_Description_Test_1",
            Address = "Establishment_Address_Test_1",
            LogoUrl = "Establishment_Logo_Url_Test_1",
            Balance = 1000,
            EstablishmentTypes = new List<EstablishmentType>() { establishmentType }
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var establishmentImage = _establishmentImageRepository.CreateAsync(new EstablishmentImage
        {
            ImageUrl = "Establishment_Image_Url_Test_1",
            EstablishmentId = establishment.Id
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var establishmentImages = _establishmentImageRepository.CreateRangeAsync(new List<EstablishmentImage>()
        {
            new()
            {
                ImageUrl = "Establishment_Image_Url_Test_2",
                EstablishmentId = establishment.Id
            },
            new()
            {
                ImageUrl = "Establishment_Image_Url_Test_3",
                EstablishmentId = establishment.Id
            }
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var employee = _employeeRepository.CreateAsync(new()
        {
            UserName = "Employee_UserName_Test_1",
            Email = "Employee_Email_Test_1@Test.com",
            PhoneNumber = "Employee_PhoneNumber_Test_1",
            AvatarUrl = "Employee_Avatar_Url_Test_1",
            DOB = new DateTime(2000, 1, 1),
            WorkingSince = DateTime.Now,
            Balance = 500,
            Salary = 100,
            EstablishmentId = establishment.Id
        }, "Employee_Password_Test_1").GetAwaiter().GetResult();

        var productType = _productTypeRepository.CreateAsync(new()
        {
            Type = "ProductType_Name_Test_1",
            Description = "ProductType_Description_Test_1",
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var product = _productRepository.CreateAsync(new()
        {
            Name = "Product_Name_Test_1",
            Description = "Product_Description_Test_1",
            ProductTypes = new List<ProductType>
            {
                new()
                {
                    Type = "ProductType_Name_Test_2",
                    Description = "ProductType_Description_Test_2",
                },
                new()
                {
                    Type = "ProductType_Name_Test_3",
                    Description = "ProductType_Description_Test_3",
                }
            },
            ProductImages = new List<ProductImage>
            {
                new()
                {
                    ImageUrl = "ProductType_Image_Url_Test_1",
                },
                new()
                {
                    ImageUrl = "ProductType_Image_Url_Test_2",
                },
                new()
                {
                    ImageUrl = "ProductType_Image_Url_Test_3",
                }
            }
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var establishment_Product = _establishment_ProductRepository.CreateAsync(new()
        {
            Quantity = 10,
            PurchasePrice = 75,
            RetailPrice = 100,
            ProductionDate = new DateTime(2020, 2, 22),
            AidAmount = 10,
            AidPercentage = 0,
            ProductId = product.Id,
            EstablishmentId = establishment.Id,
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var charity = _establishmentRepository.CreateAsync(new Establishment
        {
            Name = "Charity_Name_Test_1",
            Description = "Charity_Description_Test_1",
            Address = "Charity_Address_Test_1",
            LogoUrl = "Charity_Logo_Url_Test_1",
            Balance = 10000,
            EstablishmentTypes = new List<EstablishmentType>() { new()
            {
                Type = "Charity_Type_Test_1",
                Description = "Charity_Description_Test_1",
            }},
            EstablishmentImages = new List<EstablishmentImage>()
            {
                new()
                {
                    ImageUrl = "Charity_Image_Image_Url_Test_1",
                },
                new()
                {
                    ImageUrl = "Charity_Image_Image_Url_Test_2",
                },
                new()
                {
                    ImageUrl = "Charity_Image_Image_Url_Test_3",
                }
            }
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var charityEmployee = _employeeRepository.CreateAsync(new()
        {
            UserName = "Charity_Employee_UserName_Test_1",
            Email = "Charity_Employee_Email_Test_1@Test.com",
            PhoneNumber = "Charity_Employee_PhoneNumber_Test_1",
            AvatarUrl = "Charity_Employee_Avatar_Url_Test_1",
            DOB = new DateTime(2001, 1, 1),
            WorkingSince = DateTime.Now,
            Balance = 250,
            Salary = 50,
            EstablishmentId = charity.Id
        }, "Charity_Employee_Password_Test_1").GetAwaiter().GetResult();

        var financialAid = _financialAidRepository.CreateAsync(new()
        {
            Budget = 2500,
            AidAmount = 5,
            AidPercentage = 0,
            MinBalance = 0,
            MaxBalance = 1000,
            EstablishmentId = charity.Id,
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var productType_FinancialAid = _productType_FinancialAidRepository.CreateAsync(new()
        {
            ProductTypeId = productType.Id,
            FinancialAidId = financialAid.Id,
            EstablishmentId = establishment.Id
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var paymentService = _paymentServiceRepository.CreateAsync(new()
        {
            Name = "Payment_Service_Name_Test_1",
            Provider = "Payment_Service_Provider_Test_1",
            Fee = 2.5,
            FeePercentage = 1.5
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        var customer = _customerRepository.CreateAsync(new()
        {
            UserName = "Customer_UserName_Test_1",
            Email = "Customer_Email_Test_1@Test.com",
            Address = "Customer_Address_Test_1",
            PhoneNumber = "Customer_PhoneNumber_Test_1",
            AvatarUrl = "Customer_Avatar_Url_Test_1",
            DOB = new DateTime(2001, 1, 1),
            Balance = 500,
            Salary = 150,
        }, "Customer_Password_Test_1", new List<Claim>()).GetAwaiter().GetResult();

        var order = _orderRepository.CreateAsync(new()
        {
            Quantity = 2,
            Status = OrderStatus.Pending,
            CustomerId = customer.Id,
            Establishment_ProductId = establishment_Product.Id,
            FinancialAidId = financialAid.Id,
            EstablishmentId = establishment.Id
        }).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        order.Status = OrderStatus.Preparing;
        order = _orderRepository.UpdateAsync(order).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        order.Status = OrderStatus.Sending;
        order = _orderRepository.UpdateAsync(order).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        establishment_Product.Quantity -= order.Quantity;
        establishment_Product = _establishment_ProductRepository.UpdateAsync(establishment_Product).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        Payment payment = new()
        {
            Date = DateTime.Now,
            Status = PaymentStatus.Pending,
            PaymentServiceId = paymentService.Id,
            EstablishmentId = establishment.Id,
            Orders = new List<Order>() { order }
        };

        //var aids = payment.Orders.Select(x => (x.Establishment_Product!.AidAmount +
        //    ((x.Establishment_Product!.AidPercentage / 100) * x.Establishment_Product!.RetailPrice)));

        //var charityAids = payment.Orders.Select(x => (x.FinancialAid!.AidAmount +
        //    ((x.FinancialAid!.AidPercentage / 100) * x.Establishment_Product!.RetailPrice)));

        //var totalRetail = payment.Orders.Select(x => (x.Quantity * x.Establishment_Product?.RetailPrice));

        //var total = totalRetail.Sum() - aids.Sum() - charityAids.Sum();

        payment = _paymentRepository.CreateAsync(payment).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        payment.Status = PaymentStatus.Success;
        payment = _paymentRepository.UpdateAsync(payment).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        order.Status = OrderStatus.Delivered;
        order = _orderRepository.UpdateAsync(order).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        //establishment.Balance = (order.Quantity * (order.Establishment_Product!.RetailPrice - (order.Establishment_Product.)));

        var dbCustomersInclude = _customerRepository.GetAllAsync(includeProperties: x => x.Orders!).GetAwaiter().GetResult();
        var dbEmployeesInclude = _employeeRepository.GetAllAsync(includeProperties: x => x.Establishment!).GetAwaiter().GetResult();
        var dbEstablishmentsInclude = _establishmentRepository.GetAllAsync(false, default, x => x.FinancialAids!, x => x.Employees!, x => x.Establishment_Products!, x => x.EstablishmentTypes!, x => x.EstablishmentImages!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbEstablishmentTypesInclude = _establishmentTypeRepository.GetAllAsync(false, includeProperties: x => x.Establishments!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbEstablishmentImagesInclude = _establishmentImageRepository.GetAllAsync(false, includeProperties: x => x.Establishment!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbFinancialAidsInclude = _financialAidRepository.GetAllAsync(false, default, x => x.Establishment!, x => x.ProductType_FinancialAids!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbOrdersInclude = _orderRepository.GetAllAsync(false, default, x => x.Customer!, x => x.Establishment_Product!, x => x.FinancialAid!, x => x.Payment!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbPaymentsInclude = _paymentRepository.GetAllAsync(false, default, x => x.PaymentService!, x => x.Orders!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbPaymentServicesInclude = _paymentServiceRepository.GetAllAsync(false, includeProperties: x => x.Payments!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProductsInclude = _productRepository.GetAllAsync(false, default, x => x.ProductTypes!, x => x.ProductImages!, x => x.Establishment_Products!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProductTypesInclude = _productTypeRepository.GetAllAsync(false, default, x => x.Products!, x => x.ProductType_FinancialAids!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProductImagesInclude = _productImageRepository.GetAllAsync(false, includeProperties: x => x.Product!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbEstablishment_ProductsInclude = _establishment_ProductRepository.GetAllAsync(false, default, x => x.Establishment!, x => x.Product!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        var dbProduct_FinancialsInclude = _productType_FinancialAidRepository.GetAllAsync(false, default, x => x.ProductType!, x => x.FinancialAid!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);

        //var dbCustomersThenInclude = _customerRepository.GetAllAsync(includeProperties: x => x.Orders!).GetAwaiter().GetResult();
        //var dbEmployeesThenInclude = _employeeRepository.GetAllAsync(includeProperties: x => x.Establishment!).GetAwaiter().GetResult();
        //var dbEstablishmentsThenInclude = _establishmentRepository.GetAllAsync(default, x => x.FinancialAids!, x => x.Employees!,
        //    x => x.Establishment_Products!, x => x.EstablishmentTypes!, x => x.EstablishmentImages!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbEstablishmentTypesThenInclude = _establishmentTypeRepository.GetAllAsync(includeProperties: x => x.Establishments!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbEstablishmentImagesThenInclude = _establishmentImageRepository.GetAllAsync(includeProperties: x => x.Establishment!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbFinancialAidsThenInclude = _financialAidRepository.GetAllAsync(default, x => x.Establishment!, x => x.ProductType_FinancialAids!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbOrdersThenInclude = _orderRepository.GetAllAsync(default, x => x.Customer!, x => x.Establishment_Product!,
        //    x => x.FinancialAid!, x => x.Payment!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbPaymentsThenInclude = _paymentRepository.GetAllAsync(default, x => x.PaymentService!, x => x.Orders!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbPaymentServicesThenInclude = _paymentServiceRepository.GetAllAsync(includeProperties: x => x.Payments!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbProductsThenInclude = _productRepository.GetAllAsync(default, x => x.ProductTypes!, x => x.ProductImages!, x => x.Establishment_Products!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbProductTypesThenInclude = _productTypeRepository.GetAllAsync(default, x => x.Products!, x => x.ProductType_FinancialAids!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbProductImagesThenInclude = _productImageRepository.GetAllAsync(includeProperties: x => x.Product!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbEstablishment_ProductsThenInclude = _establishment_ProductRepository.GetAllAsync(default, x => x.Establishment!, x => x.Product!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
        //var dbProduct_FinancialsThenInclude = _productType_FinancialAidRepository.GetAllAsync(default, x => x.ProductType!, x => x.FinancialAid!).GetAwaiter().GetResult().Match(succ => succ, excp => null!);
    }
}
