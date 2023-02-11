using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;
using FishForoshi.Implementation.Tools;

namespace FishForoshi.Implementation
{
    public class GetNorm : IGetNorm
    {
        private readonly IQueryRepository<Norm> _query;
        private readonly IQueryRepository<Food> _foodQuery;

        public GetNorm(IQueryRepository<Norm> query, IQueryRepository<Food> foodQuery)
        {
            _query = query;
            _foodQuery = foodQuery;
        }

        public async Task<Norm> GetByIdAsync(Guid id)
            => await _query.GetAsync(id);

        public async Task<IPagedList<Norm>> GetNormsAsync(int pageNumber, string name, Guid foodId)
        {
            var norms = await _query.GetAllAsync(x => x.FoodId == foodId);
            if (name != null)
            {
                norms = norms.Where(x => x.Name.Contains(name));
            }
            return new PagedList<Norm>(norms, pageNumber, 10);
        }
    }
}
