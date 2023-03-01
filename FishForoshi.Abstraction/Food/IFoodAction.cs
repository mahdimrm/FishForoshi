using FishForoshi.Entities;
using Microsoft.AspNetCore.Http;

namespace FishForoshi.Abstraction
{
    public interface IFoodAction
    {
        Task<FoodActionStatus> CreateAsync(Food food, IFormFile file);
        Task<FoodActionStatus> UpdateAsync(Food food, IFormFile file);
        Task<FoodActionStatus> DeleteAsync(Guid id);
    }
}
