using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class FactorAction : IFactorAction
    {
        private readonly ICudRepository<Factor> _action;
        private readonly IQueryRepository<Factor> _query;

        public FactorAction(ICudRepository<Factor> action, IQueryRepository<Factor> query)
        {
            _action = action;
            _query = query;
        }

        public async Task<FactorActionStatus> CreateAsync(Factor factor)
        {
            return await _action.AddAsync(factor) ?
             FactorActionStatus.Success : FactorActionStatus.Failed;
        }

        public async Task<FactorActionStatus> DeleteAsync(Guid id)
                => await _action.DeleteByIdAsync(id)
                                ? FactorActionStatus.Success
                                : FactorActionStatus.Failed;

        public async Task<FactorActionStatus> UpdateAsync(Factor factor)
        {
            var model = await _query.GetAsync(factor.Id);

            model.Price = factor.Price;
            model.DailyFoodId = factor.DailyFoodId;
            model.Passed = factor.Passed;

            return await _action.UpdateAsync(model)
                ? FactorActionStatus.Success : FactorActionStatus.Failed;
        }
    }
}
