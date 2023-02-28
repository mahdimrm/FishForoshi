using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class DayAction : IDayAction
    {
        private readonly IQueryRepository<Day> _query;
        private readonly ICudRepository<Day> _action;

        public DayAction(IQueryRepository<Day> query, ICudRepository<Day> action)
        {
            _query = query;
            _action = action;
        }

        public async Task<DayActionStatus> CreateAsync(Day day)
        {
            var result = await _action.AddAsync(day);
                      return result ? DayActionStatus.Success : DayActionStatus.Failed;
        }

        public async Task<DayActionStatus> DeleteAsync(Guid id)
            => await _action.DeleteByIdAsync(id)
                                    ? DayActionStatus.Success
                                    : DayActionStatus.Failed;

        public async Task<DayActionStatus> UpdateAsync(Day day)
        {
            var model = await _query.GetAsync(day.Id);

            model.Name = day.Name;
            model.Date = day.Date;

            return await _action.UpdateAsync(model)
                ? DayActionStatus.Success : DayActionStatus.Failed;
        }
    }
}
