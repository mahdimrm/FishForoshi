using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class DailyFoodAction : IDailyFoodAction
    {
        private readonly IQueryRepository<DailyFood> _query;
        private readonly ICudRepository<DailyFood> _action;
        private readonly IGetFactor _factorQuery;

        public DailyFoodAction
            (IQueryRepository<DailyFood> query,
            ICudRepository<DailyFood> action,
            IGetFactor factorQuery)
        {
            _query = query;
            _action = action;
            _factorQuery = factorQuery;
        }

        public async Task<DailyFoodStatus> CreateAsync(DailyFood dailyFood)
        {
            var result = await _action.AddAsync(dailyFood);
            return result ? DailyFoodStatus.Success : DailyFoodStatus.Failed;
        }

        public async Task<DailyFoodStatus> DeleteAsync(Guid id)
            => await _action.DeleteByIdAsync(id)
                                    ? DailyFoodStatus.Success
                                    : DailyFoodStatus.Failed;

        public async Task<DailyFoodStatus> UpdateAsync(DailyFood dailyFood)
        {
            var model = await _query.GetAsync(dailyFood.Id);
            var factorCounts = await _factorQuery.GetDailyFoodTotal(model.Id);
            if (dailyFood.Count < factorCounts)
            {
                return DailyFoodStatus.HaveFactor;
            }

            model.FoodId = dailyFood.FoodId;
            model.DayId = dailyFood.DayId;
            model.Count = dailyFood.Count;
            model.Date = dailyFood.Date;



            return await _action.UpdateAsync(model)
                ? DailyFoodStatus.Success : DailyFoodStatus.Failed;
        }
    }
}
