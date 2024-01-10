using AutoMapper;
using Common.Classes;
using DTO.DTOs.EstablishmentDTOs;
using DTO.DTOs.FinancialAidDTOs;
using DTO.DTOs.OrderDTOs;
using DTO.DTOs.PaymentDTOs;
using DTO.DTOs.ProductDTOs;
using DTO.DTOs.SharedDTOs;
using DataAccess.Models.SharedModels;
using DataAccess.Models.OrderModels;
using DataAccess.Models.ProductModels;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Models.PaymentModels;
using DataAccess.Models.EstablishmentModels;
using DTO.DTOs.BaseDTOs;

namespace Business.Helpers.Mappers;

public class ModelDTOProfile : Profile
{
    public ModelDTOProfile()
    {
        //CreateMap<BaseEntity, BaseDTO>().ReverseMap();
        //CreateMap<SoftDeletableEntity, SoftDeletableEntityDTO>().IncludeBase<BaseEntity, BaseDTO>().ReverseMap();
        //CreateMap<OwnableEntity, OwnableEntityDTO>().IncludeBase<SoftDeletableEntity, SoftDeletableEntityDTO>().ReverseMap();
        //CreateMap<BaseImage, BaseImageDTO>().IncludeBase<SoftDeletableEntity, SoftDeletableEntityDTO>().ReverseMap();
        //CreateMap<BaseType, BaseTypeDTO>().IncludeBase<SoftDeletableEntity, SoftDeletableEntityDTO>().ReverseMap();

        CreateMap<Establishment, EstablishmentDTO>().ForMember(x => x.EmployeeDTOs, y => y.MapFrom(z => z.Employees))
                                                    .ForMember(x => x.EstablishmentTypeDTOs, y => y.MapFrom(z => z.EstablishmentTypes))
                                                    .ForMember(x => x.EstablishmentImageDTOs, y => y.MapFrom(z => z.EstablishmentImages))
                                                    .ForMember(x => x.FinancialAidDTOs, y => y.MapFrom(z => z.FinancialAids))
                                                    .ForMember(x => x.Establishment_ProductDTOs, y => y.MapFrom(z => z.Establishment_Products))
                                                    .ReverseMap();
                                                    //.IncludeBase<SoftDeletableEntity, SoftDeletableEntityDTO>()

        CreateMap<EstablishmentType, EstablishmentTypeDTO>().ForMember(x => x.EstablishmentDTOs, y => y.MapFrom(z => z.Establishments))
                                                            .ReverseMap();
                                                            //.IncludeBase<BaseType, BaseTypeDTO>()

        CreateMap<EstablishmentImage, EstablishmentImageDTO>().ForMember(x => x.EstablishmentDTO, y => y.MapFrom(z => z.Establishment))
                                                              .ReverseMap();
                                                              //.IncludeBase<BaseImage, BaseImageDTO>()

        CreateMap<Product, ProductDTO>().ForMember(x => x.ProductTypeDTOs, y => y.MapFrom(z => z.ProductTypes))
                                        .ForMember(x => x.ProductImageDTOs, y => y.MapFrom(z => z.ProductImages))
                                        .ForMember(x => x.Establishment_ProductDTOs, y => y.MapFrom(z => z.Establishment_Products))
                                        .ReverseMap();
                                        //.IncludeBase<SoftDeletableEntity, SoftDeletableEntityDTO>()

        CreateMap<ProductType, ProductTypeDTO>().ForMember(x => x.ProductDTOs, y => y.MapFrom(z => z.Products))
                                                .ReverseMap();
                                                //.IncludeBase<BaseType, BaseTypeDTO>()

        CreateMap<ProductImage, ProductImageDTO>().ForMember(x => x.ProductDTO, y => y.MapFrom(z => z.Product))
                                                  .ReverseMap();
                                                  //.IncludeBase<BaseImage, BaseImageDTO>()

        CreateMap<FinancialAid, FinancialAidDTO>().ForMember(x => x.EstablishmentDTO, y => y.MapFrom(z => z.Establishment))
                                                  .ForMember(x => x.OrderDTOs, y => y.MapFrom(z => z.Orders))
                                                  .ForMember(x => x.ProductType_FinancialAidDTOs, y => y.MapFrom(z => z.ProductType_FinancialAids))
                                                  .ReverseMap();
                                                  //.IncludeBase<SoftDeletableEntity, SoftDeletableEntityDTO>()

        CreateMap<Order, OrderDTO>().ForMember(x => x.CustomerDTO, y => y.MapFrom(z => z.Customer))
                                    .ForMember(x => x.PaymentDTO, y => y.MapFrom(z => z.Payment))
                                    .ForMember(x => x.FinancialAidDTO, y => y.MapFrom(z => z.FinancialAid))
                                    .ForMember(x => x.Establishment_ProductDTO, y => y.MapFrom(z => z.Establishment_Product))
                                    .ForMember(x => x.Status, y => y.MapFrom(z => z.Status))
                                    .ReverseMap();
                                    //.IncludeBase<OwnableEntity, OwnableEntityDTO>()

        CreateMap<Payment, PaymentDTO>().ForMember(x => x.PaymentServiceDTO, y => y.MapFrom(z => z.PaymentService))
                                        .ForMember(x => x.OrderDTOs, y => y.MapFrom(z => z.Orders))
                                        .ForMember(x => x.Status, y => y.MapFrom(z => z.Status))
                                        .ReverseMap();
                                        //.IncludeBase<OwnableEntity, OwnableEntityDTO>()

        CreateMap<PaymentService, PaymentServiceDTO>().ForMember(x => x.PaymentDTOs, y => y.MapFrom(z => z.Payments))
                                                      .ReverseMap();
                                                      //.IncludeBase<SoftDeletableEntity, SoftDeletableEntityDTO>()

        CreateMap<Establishment_Product, Establishment_ProductDTO>().ForMember(x => x.OrderDTOs, y => y.MapFrom(z => z.Orders))
                                                                    .ForMember(x => x.ProductDTO, y => y.MapFrom(z => z.Product))
                                                                    .ForMember(x => x.EstablishmentDTO, y => y.MapFrom(z => z.Establishment))
                                                                    .ReverseMap();
                                                                    //.IncludeBase<OwnableEntity, OwnableEntityDTO>()
        CreateMap<ProductType_FinancialAid, ProductType_FinancialAidDTO>().ForMember(x => x.ProductTypeDTO, y => y.MapFrom(z => z.ProductType))
                                                                    .ForMember(x => x.FinancialAidDTO, y => y.MapFrom(z => z.FinancialAid))
                                                                    .ReverseMap();
                                                                    //.IncludeBase<OwnableEntity, OwnableEntityDTO>()
    }
}
