using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IGetFactor
    {
        Task<IPagedList<Factor>> GetAsync(int page);
        Task<Factor> GetAsync(Guid id);
    }
}
