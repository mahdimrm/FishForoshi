using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class GetDay : IGetDay
    {
        private readonly IQueryRepository<Day> _query;

        public GetDay(IQueryRepository<Day> query)
        {
            _query = query;
        }

        public async Task<IEnumerable<Day>> GetAsync()
            => await _query.GetAllAsync();

        public async Task<Day> GetAsync(Guid id)
            => await _query.GetAsync(id);
    }
}
