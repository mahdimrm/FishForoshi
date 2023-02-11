using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class NormAction : INormAction
    {
        private readonly IQueryRepository<Norm> _query;
        private readonly ICudRepository<Norm> _action;

        public NormAction(IQueryRepository<Norm> query, ICudRepository<Norm> action)
        {
            _query = query;
            _action = action;
        }
        public async Task<NormActionStatus> CreateAsync(Norm norm)
        {
            return await _action.AddAsync(norm) ?
             NormActionStatus.Success : NormActionStatus.Failed;
        }
        public async Task<NormActionStatus> DeleteAsync(Guid id)
                => await _action.DeleteByIdAsync(id)
                                ? NormActionStatus.Success
                                : NormActionStatus.Failed;

        public async Task<NormActionStatus> UpdateAsync(Norm norm)
        {
            var model = await _query.GetAsync(norm.Id);

            model.Name = norm.Name;
            model.Value = norm.Value;
            model.FoodId = norm.FoodId;

            return await _action.UpdateAsync(model)
                ? NormActionStatus.Success : NormActionStatus.Failed;
        }

        public async Task<NormActionStatus> UpsertAsync(Norm norm)
            => norm?.Id is null
            ? await CreateAsync(norm) : await UpdateAsync(norm);
    }
}
