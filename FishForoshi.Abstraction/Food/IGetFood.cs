using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IGetFood
    {
        Task<IPagedList<Food>> GetFoodsAsync(int pageNumber, string name);

        Task<Food> GetByIdAsync(Guid id);
    }
}
