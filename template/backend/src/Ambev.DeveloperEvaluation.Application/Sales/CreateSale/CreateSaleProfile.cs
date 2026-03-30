using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.Dtos;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSale operations.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<CustomerDto, Customer>();
        CreateMap<BranchDto, Branch>();
        CreateMap<SaleItemDto, SaleItem>();
        CreateMap<ProductDto, Product>();

        CreateMap<Sale, CreateSaleResult>();
    }
}