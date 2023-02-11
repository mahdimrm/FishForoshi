using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class FoodAction : IFoodAction
    {
        private readonly IQueryRepository<Food> _query;
        private readonly ICudRepository<Food> _action;

        public FoodAction(IQueryRepository<Food> query, ICudRepository<Food> action)
        {
            _query = query;
            _action = action;
        }

        public async Task<FoodActionStatus> CreateAsync(Food food)
        {
            return await _action.AddAsync(food) ?
             FoodActionStatus.Success : FoodActionStatus.Failed;
        }



        public async Task<FoodActionStatus> DeleteAsync(Guid id)
                => await _action.DeleteByIdAsync(id)
                                ? FoodActionStatus.Success
                                : FoodActionStatus.Failed;

        public async Task<FoodActionStatus> UpdateAsync(Food food)
        {
            var model = await _query.GetAsync(food.Id);

            model.Name = food.Name;
            model.FoodType = food.FoodType;
            model.Price = food.Price;

            return await _action.UpdateAsync(model)
                ? FoodActionStatus.Success : FoodActionStatus.Failed;
        }

        public async Task<FoodActionStatus> UpsertAsync(Food food)
            => food?.Id is null
            ? await CreateAsync(food) : await UpdateAsync(food);
    }
}
