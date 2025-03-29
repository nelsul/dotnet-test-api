using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTestApi.Data;
using MyTestApi.DTOs.Stock;
using MyTestApi.Interfaces;
using MyTestApi.Mappers;

namespace MyTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly IWebHostEnvironment _env;

        private readonly string _uploadPath;

        public StockController(IStockRepository stockRepo, IWebHostEnvironment env)
        {
            _stockRepo = stockRepo;
            _env = env;

            _uploadPath = "/Users/nelsonneto/dev/dotnet/MyTest/uploads";
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();

            var stockDTOs = stocks.Select(s => s.ToStockDTO());

            return Ok(stockDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDTO createStockDTO)
        {
            var stock = createStockDTO.ToStock();

            await _stockRepo.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDTO());
        }

        [HttpPost("image-upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImageUpload([FromForm] StockImageDTO stockImageDTO)
        {
            var file = stockImageDTO.Image;

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file sent.");
            }

            try
            {
                var uploadPath = _uploadPath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { FileName = fileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar a imagem: {ex.Message}");
            }
        }
    }
}
