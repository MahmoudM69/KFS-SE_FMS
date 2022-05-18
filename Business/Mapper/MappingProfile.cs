using AutoMapper;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.PaymentModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.FinancialAidDTOs;
using Models.DTOModels.OrderDTOs;
using Models.DTOModels.PaymentDTOs;
using Models.DTOModels.ProductDTOs;
using Models.DTOModels.SharedDTOs;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Establishment, EstablishmentDTO>().ReverseMap().ForMember(x => x.Employees, y => y.MapFrom(z => z.EmployeeDTOs))
                                                                     .ForMember(x => x.EstablishmentType, y => y.MapFrom(z => z.EstablishmentTypeDTO))
                                                                     .ForMember(x => x.EstablishmentImages, y => y.MapFrom(z => z.EstablishmentImageDTOs))
                                                                     .ForMember(x => x.FinancialAids, y => y.MapFrom(z => z.FinancialAidDTOs))
                                                                     .ForMember(x => x.Establishment_Products, y => y.MapFrom(z => z.Establishment_ProductDTOs));

            CreateMap<EstablishmentType, EstablishmentTypeDTO>().ReverseMap().ForMember(x => x.Establishments, y => y.MapFrom(z => z.EstablishmentDTOs));

            CreateMap<EstablishmentImage, EstablishmentImageDTO>().ReverseMap().ForMember(x => x.Establishment, y => y.MapFrom(z => z.EstablishmentDTO));

            CreateMap<Product, ProductDTO>().ReverseMap().ForMember(x => x.ProductType, y => y.MapFrom(z => z.ProductTypeDTO))
                                                         .ForMember(x => x.Orders, y => y.MapFrom(z => z.OrderDTOs))
                                                         .ForMember(x => x.ProductImages, y => y.MapFrom(z => z.ProductImageDTOs))
                                                         .ForMember(x => x.Establishment_Products, y => y.MapFrom(z => z.Establishment_ProductDTOs));

            CreateMap<ProductType, ProductTypeDTO>().ReverseMap().ForMember(x => x.Products, y => y.MapFrom(z => z.ProductDTOs));

            CreateMap<ProductImage, ProductImageDTO>().ReverseMap().ForMember(x => x.Product, y => y.MapFrom(z => z.ProductDTO));

            CreateMap<FinancialAid, FinancialAidDTO>().ReverseMap().ForMember(x => x.Establishment, y => y.MapFrom(z => z.EstablishmentDTO))
                                                                   .ForMember(x => x.Orders, y => y.MapFrom(z => z.OrderDTOs));

            CreateMap<Order, OrderDTO>().ReverseMap().ForMember(x => x.Customer, y => y.MapFrom(z => z.CustomerDTO))
                                                     .ForMember(x => x.FinancialAid, y => y.MapFrom(z => z.FinancialAidDTO))
                                                     .ForMember(x => x.Payment, y => y.MapFrom(z => z.PaymentDTO))
                                                     .ForMember(x => x.Establishment_Product, y => y.MapFrom(z => z.Establishment_ProductDTO));

            CreateMap<Payment, PaymentDTO>().ReverseMap().ForMember(x => x.PaymentService, y => y.MapFrom(z => z.PaymentServiceDTO))
                                                         .ForMember(x => x.Orders, y => y.MapFrom(z => z.OrderDTOs));

            CreateMap<PaymentService, PaymentServiceDTO>().ReverseMap().ForMember(x => x.Payments, y => y.MapFrom(z => z.PaymentDTOs));

            CreateMap<Establishment_Product, Establishment_ProductDTO>().ReverseMap().ForMember(x => x.Orders, y => y.MapFrom(z => z.OrderDTOs))
                                                                                     .ForMember(x => x.Product, y => y.MapFrom(z => z.ProductDTO))
                                                                                     .ForMember(x => x.Establishment, y => y.MapFrom(z => z.EstablishmentDTO));
        }
    }
}
