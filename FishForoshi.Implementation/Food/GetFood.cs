using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;
using FishForoshi.Implementation.Tools;
using System.Security.Cryptography.X509Certificates;

namespace FishForoshi.Implementation
{
    public class GetFood : IGetFood
    {
        private readonly IQueryRepository<Food> _query;

        public GetFood(IQueryRepository<Food> query)
        {
            _query = query;
        }

        public async Task<Food> GetByIdAsync(Guid id)
            => await _query.GetAsync(id);

        public async Task<IPagedList<Food>> GetFoodsAsync(int pageNumber, string name)
        {
            var foods = await _query.GetAllAsync();

            if (name != null)
            {
                foods = foods.Where(x => x.Name.Contains(name));
            }
            return new PagedList<Food>(foods, pageNumber, 10);
        }
    }
}
