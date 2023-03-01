using FishForoshi.Entities;
using FishForoshi.ViewModel.DailyFood;

namespace FishForoshi.Abstraction
{
    public interface IGetDailyFood
    {
        Task<IEnumerable<DailyFoodViewModel>> GetAsync();
        Task<DailyFood> GetAsync(Guid id);
    }
}
