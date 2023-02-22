using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IGetCustomerTurn
    {
        Task<IPagedList<CustomerTurn>> Get(int page, string barberName, string customerName, string day);
        Task<IEnumerable<CustomerTurn>> GetTurnsByBarberId(Guid barberId);
        Task<CustomerTurn> GetAsync(Guid? id);
    }
}
