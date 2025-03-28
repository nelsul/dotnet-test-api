using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestApi.DTOs.Stock
{
    public class CreateStockDTO
    {
        [Required]
        [Length(4, 4, ErrorMessage = "Title must have 4 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [Length(3, 50, ErrorMessage = "Company Name lenght must be between 3 and 50")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(
            0.01,
            (double)decimal.MaxValue,
            ErrorMessage = "Purchase must be worth more than $0.01"
        )]
        public decimal Purchase { get; set; }

        [Required]
        [Range(
            0.01,
            (double)decimal.MaxValue,
            ErrorMessage = "Last Dividend must be worth more than $0.01"
        )]
        public decimal LastDiv { get; set; }

        [Required]
        [Length(3, 50, ErrorMessage = "Industry character lenght must be between 3 and 50")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Market Capital must be worth more than $1")]
        public long MarketCap { get; set; }
    }
}
