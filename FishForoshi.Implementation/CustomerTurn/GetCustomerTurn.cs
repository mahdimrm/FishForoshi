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

        public async Task<IPagedList<CustomerTurn>> Get(int page, string barberName, string customerName, string time)
        {
            var turns = await _query.GetAllAsync();

            turns = turns.Where(x => x.InsertDateTime.Day == DateTime.Today.Day);

            if (time != null)
            {
                turns = turns.Where(x => x.CustomerQueueTime.Contains(time));
            }

            if (barberName != null)
            {
                turns = turns.Where(x => x.BarberStaff.FullName.Contains(barberName));
            }

            if (customerName != null)
            {
                turns = turns.Where(x => x.CustomerName.Contains(customerName));
            }

            return new PagedList<CustomerTurn>(turns, page, 10);
        }

        public async Task<CustomerTurn> GetAsync(Guid? id)
            => await _query.GetAsync(id);

        public async Task<IEnumerable<CustomerTurn>> GetTurnsByBarberId(Guid barberId)
            => await _query.GetAllAsync(x => x.BarberStaffId == barberId);
    }
}
