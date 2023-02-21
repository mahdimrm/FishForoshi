using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IGetNorm
    {
        Task<IEnumerable<Norm>> GetNormsAsync(Guid id);

        Task<Norm> GetByIdAsync(Guid id);
        Task<IEnumerable<Norm>> Norms(Guid foodId);
    }
}
