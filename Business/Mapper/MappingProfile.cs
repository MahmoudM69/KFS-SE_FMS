using AutoMapper;
using DataAcesss.Data.CustomerModels;
using DataAcesss.Data.EmployeeModels;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.PaymentModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Establishment, Establishment>()
                .ReverseMap()
                .ForMember(x => x.Employees, y => y.MapFrom(z => z.Employees))
                                                                     .ForMember(x => x.EstablishmentType, y => y.MapFrom(z => z.EstablishmentType))
                                                                     .ForMember(x => x.EstablishmentImages, y => y.MapFrom(z => z.EstablishmentImages))
                                                                     .ForMember(x => x.FinancialAids, y => y.MapFrom(z => z.FinancialAids))
                                                                     .ForMember(x => x.Establishment_Products, y => y.MapFrom(z => z.Establishment_Products));

            CreateMap<EstablishmentType, EstablishmentType>().ReverseMap().ForMember(x => x.Establishments, y => y.MapFrom(z => z.Establishments));

            CreateMap<EstablishmentImage, EstablishmentImage>().ReverseMap().ForMember(x => x.Establishment, y => y.MapFrom(z => z.Establishment));

            CreateMap<Employee, Employee>().ReverseMap().ForMember(x => x.Establishment, y => y.MapFrom(z => z.Establishment));

            CreateMap<Customer, Customer>().ReverseMap();

            CreateMap<Product, Product>().ReverseMap().ForMember(x => x.ProductType, y => y.MapFrom(z => z.ProductType))
                                                         .ForMember(x => x.Orders, y => y.MapFrom(z => z.Orders))
                                                         .ForMember(x => x.ProductImages, y => y.MapFrom(z => z.ProductImages))
                                                         .ForMember(x => x.Establishment_Products, y => y.MapFrom(z => z.Establishment_Products));

            CreateMap<ProductType, ProductType>().ReverseMap().ForMember(x => x.Products, y => y.MapFrom(z => z.Products));

            CreateMap<ProductImage, ProductImage>().ReverseMap().ForMember(x => x.Product, y => y.MapFrom(z => z.Product));

            CreateMap<FinancialAid, FinancialAid>().ReverseMap().ForMember(x => x.Establishment, y => y.MapFrom(z => z.Establishment))
                                                                   .ForMember(x => x.Orders, y => y.MapFrom(z => z.Orders));

            CreateMap<Order, Order>().ReverseMap().ForMember(x => x.Customer, y => y.MapFrom(z => z.Customer))
                                                     .ForMember(x => x.FinancialAid, y => y.MapFrom(z => z.FinancialAid))
                                                     .ForMember(x => x.Payment, y => y.MapFrom(z => z.Payment))
                                                     .ForMember(x => x.establishment_Product, y => y.MapFrom(z => z.establishment_Product));

            CreateMap<Payment, Payment>().ReverseMap().ForMember(x => x.PaymentService, y => y.MapFrom(z => z.PaymentService))
                                                         .ForMember(x => x.Orders, y => y.MapFrom(z => z.Orders));

            CreateMap<PaymentService, PaymentService>().ReverseMap().ForMember(x => x.Payments, y => y.MapFrom(z => z.Payments));

            CreateMap<Establishment_Product, Establishment_Product>().ReverseMap().ForMember(x => x.Orders, y => y.MapFrom(z => z.Orders))
                                                                                     .ForMember(x => x.Product, y => y.MapFrom(z => z.Product))
                                                                                     .ForMember(x => x.Establishment, y => y.MapFrom(z => z.Establishment));
        }
    }
}
