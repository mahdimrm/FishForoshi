using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface INormAction
    {
        Task<NormActionStatus> CreateAsync(Norm norm);
        Task<NormActionStatus> UpdateAsync(Norm norm);
        Task<NormActionStatus> UpsertAsync(Norm norm);
        Task<NormActionStatus> DeleteAsync(Guid id);
    }
}
