using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class GetBarber : IGetBarber
    {
        private readonly IQueryRepository<Barber> _query;

        public GetBarber(IQueryRepository<Barber> query)
        {
            _query = query;
        }

        public async Task<IEnumerable<Barber>> GetAsync()
            => await _query.GetAllAsync();

        public async Task<Barber> GetAsync(Guid? id)
            => await _query.GetAsync(id);
    }
}
