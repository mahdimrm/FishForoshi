using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IDailyFoodAction
    {
        Task<DailyFoodStatus> CreateAsync(DailyFood dailyFood);
        Task<DailyFoodStatus> UpdateAsync(DailyFood dailyFood);
        Task<DailyFoodStatus> DeleteAsync(Guid id);
    }
}
