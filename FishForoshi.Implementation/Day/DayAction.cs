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

        public Task<DayActionStatus> CreateAsync(Day day)
        {
            throw new NotImplementedException();
        }

        public Task<DayActionStatus> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DayActionStatus> UpdateAsync(Day day)
        {
            throw new NotImplementedException();
        }

        public Task<DayActionStatus> UpsertAsync(Day day)
        {
            throw new NotImplementedException();
        }
    }
}
