using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTestApi.DTOs.Stock;
using MyTestApi.Models;

namespace MyTestApi.Mappers
{
    public static class StockMappers
    {
        public static StockDTO ToStockDTO(this Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                CreatedAt = stock.CreatedAt,
            };
        }

        public static Stock ToStock(this CreateStockDTO createStockDTO)
        {
            return new Stock
            {
                Symbol = createStockDTO.Symbol,
                CompanyName = createStockDTO.CompanyName,
                Purchase = createStockDTO.Purchase,
                LastDiv = createStockDTO.LastDiv,
                Industry = createStockDTO.Industry,
                MarketCap = createStockDTO.MarketCap,
            };
        }
    }
}
