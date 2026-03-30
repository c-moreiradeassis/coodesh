using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleResult, CreateSaleResponse>();

        CreateMap<CustomerDto, Application.Sales.Dtos.CustomerDto>();
        CreateMap<BranchDto, Application.Sales.Dtos.BranchDto>();
        CreateMap<SaleItemDto, Application.Sales.Dtos.SaleItemDto>();
        CreateMap<ProductDto, Application.Sales.Dtos.ProductDto>();
    }
}
