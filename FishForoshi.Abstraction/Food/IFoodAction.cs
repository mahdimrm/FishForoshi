using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IFoodAction
    {
        Task<FoodActionStatus> CreateAsync(Food food);
        Task<FoodActionStatus> UpdateAsync(Food food);
        Task<FoodActionStatus> UpsertAsync(Food food);
        Task<FoodActionStatus> DeleteAsync(Guid id);
    }
}
