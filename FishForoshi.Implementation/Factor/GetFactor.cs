using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;
using FishForoshi.Implementation.Tools;

namespace FishForoshi.Implementation
{
    public class GetFactor : IGetFactor
    {
        private readonly IQueryRepository<Factor> _query;

        public GetFactor(IQueryRepository<Factor> query)
        {
            _query = query;
        }

        public async Task<IPagedList<Factor>> GetAsync(int page)
        {
            var factors = await _query.GetAllAsync();

            return new PagedList<Factor>(factors, page, 10);
        }

        public async Task<Factor> GetAsync(Guid id)
            => await _query.GetAsync(id);

        public async Task<int> GetDailyFoodTotal(Guid dailyId)
        {
            var result = await _query.GetAllAsync(x => x.DailyFoodId == dailyId);
            return result.Count();
        }
    }
}
