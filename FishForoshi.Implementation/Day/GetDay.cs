using FishForoshi.Abstraction;
using FishForoshi.Entities;
using FishForoshi.ViewModel.Common;

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

        public async Task<IEnumerable<SelectListDto>> GetNames()
        {
            var days = await _query.GetAllAsync();

            var result = days
                .Select(x => new SelectListDto
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });

            return result;
        }
    }
}
