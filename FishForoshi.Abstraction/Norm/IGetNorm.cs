using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IGetNorm
    {
        Task<IPagedList<Norm>> GetNormsAsync(int pageNumber, string name,Guid id);

        Task<Norm> GetByIdAsync(Guid id);
    }
}
