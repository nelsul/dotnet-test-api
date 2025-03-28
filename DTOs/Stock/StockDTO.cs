using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestApi.DTOs.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
