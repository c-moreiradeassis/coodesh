using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request.
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale result</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating sale with number {SaleNumber}", command.SaleNumber);

        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Check if sale number already exists
        var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingSale != null)
        {
            throw new InvalidOperationException($"Sale with number {command.SaleNumber} already exists");
        }

        var sale = _mapper.Map<Sale>(command);
        sale.Id = Guid.NewGuid();
        sale.CreatedAt = DateTime.UtcNow;

        // Apply business rules for each item
        foreach (var item in sale.Items)
        {
            item.Id = Guid.NewGuid();
            item.SaleId = sale.Id;
            item.ValidateBusinessRules();
            item.ApplyDiscount();
        }

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        _logger.LogInformation("Sale created successfully with ID {SaleId}", createdSale.Id);

        // Log event (simulating event publishing)
        var saleCreatedEvent = new SaleCreatedEvent(createdSale);
        _logger.LogInformation("SaleCreated event: Sale {SaleId} with number {SaleNumber} created at {CreatedAt}",
            saleCreatedEvent.Sale.Id, saleCreatedEvent.Sale.SaleNumber, saleCreatedEvent.Sale.CreatedAt);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}