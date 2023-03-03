using FishForoshi.Entities;
using FishForoshi.ViewModel.DailyFood;

namespace FishForoshi.Abstraction
{
    public interface IGetDailyFood
    {
        Task<FactorDailyFoodViewModel> GetAsync();
        Task<DailyFood> GetAsync(Guid id);
    }
}
