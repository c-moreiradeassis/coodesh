using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.Dtos
{
    public class SaleItemDto
    {
        public ProductDto Product { get; set; } = new();
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
