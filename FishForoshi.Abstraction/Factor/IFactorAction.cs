using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IFactorAction
    {
        Task<FactorActionStatus> CreateAsync(Factor factor);
        Task<FactorActionStatus> UpdateAsync(Factor factor);
        Task<FactorActionStatus> DeleteAsync(Guid id);
    }
}
