using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;
using FishForoshi.Implementation.Tools;
using System.Xml.Linq;

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

        public async Task<IEnumerable<Norm>> GetNormsAsync(Guid foodId)
            => await _query.GetAllAsync(x => x.FoodId == foodId);


        public async Task<IEnumerable<Norm>> Norms(Guid foodId)
        {
            var norms = await _query.GetAllAsync(x => x.FoodId == foodId);
            return norms;
        }

    }
}
