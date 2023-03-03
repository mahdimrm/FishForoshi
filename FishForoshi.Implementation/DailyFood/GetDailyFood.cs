using FishForoshi.Abstraction;
using FishForoshi.Entities;
using FishForoshi.ViewModel.DailyFood;
using Microsoft.AspNetCore.Http;

namespace FishForoshi.Implementation
{
    public class GetDailyFood : IGetDailyFood
    {
        private readonly IQueryRepository<DailyFood> _query;
        private readonly IGetFactor _factorQuery;

        public GetDailyFood(IQueryRepository<DailyFood> query, IGetFactor factorQuery)
        {
            _query = query;
            _factorQuery = factorQuery;
        }

        public async Task<FactorDailyFoodViewModel> GetAsync()
        {
            var dailies = await _query.GetAllAsync();
            var dailyFoods = dailies.Select(x => new DailyFoodViewModel
            {
                Id = x.Id,
                Day = x.Day.Name,
                Date = x.Date,
                Food = x.Food.Name,
                Count = x.Count,
                Total = (_factorQuery.GetDailyFoodTotal(x.Id).Result) - (x.Count),
                ImageName = x.Food.ImageName
            });

            FactorDailyFoodViewModel result = new() { DailyFood = dailyFoods };
            return result;
        }

        public async Task<DailyFood> GetAsync(Guid id)
            => await _query.GetAsync(id);
    }
}
