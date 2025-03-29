using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestApi.DTOs.Stock
{
    public class StockImageDTO
    {
        public IFormFile? Image { get; set; }
    }
}
