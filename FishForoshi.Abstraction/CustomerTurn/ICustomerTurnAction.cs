using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface ICustomerTurnAction
    {
        Task<CustomerTurnActionStatus> CreateAsync(CustomerTurn turn);
        Task<CustomerTurnActionStatus> UpdateAsync(CustomerTurn turn);
        Task<CustomerTurnActionStatus> UpsertAsync(CustomerTurn turn);
        Task<CustomerTurnActionStatus> DeleteAsync(Guid id);
    }
}
