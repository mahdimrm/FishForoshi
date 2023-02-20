using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;
using FishForoshi.Implementation.Tools;

namespace FishForoshi.Implementation
{
    public class GetCustomerTurn : IGetCustomerTurn
    {
        private readonly IQueryRepository<CustomerTurn> _query;

        public GetCustomerTurn(IQueryRepository<CustomerTurn> query)
        {
            _query = query;
        }

        public async Task<IPagedList<CustomerTurn>> Get(int page, DateTime day)
        {
            var turns = await _query.GetAllAsync();

            turns = turns.Where(x => x.CustomerQueueTime == day);

            return new PagedList<CustomerTurn>(turns, page, 10);
        }

        public async Task<CustomerTurn> GetAsync(Guid? id)
            => await _query.GetAsync(id);

        public async Task<IEnumerable<CustomerTurn>> GetTurnsByBarberId(Guid barberId)
            => await _query.GetAllAsync(x => x.BarberStaffId == barberId);
    }
}
