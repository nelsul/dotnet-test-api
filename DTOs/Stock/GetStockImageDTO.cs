using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestApi.DTOs.Stock
{
    public class GetStockImageDTO
    {
        [Required]
        public string? Filename { get; set; }

        [Required]
        public uint? Width { get; set; }
        public uint? Height { get; set; }
    }
}
