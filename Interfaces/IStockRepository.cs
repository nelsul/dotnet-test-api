using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTestApi.Models;

namespace MyTestApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stock);
    }
}
