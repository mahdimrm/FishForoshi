using FishForoshi.Entities;
using FishForoshi.ViewModel.Common;

namespace FishForoshi.Abstraction
{
    public interface IGetDay
    {
        Task<IEnumerable<Day>> GetAsync();
        Task<Day> GetAsync(Guid id);
        Task<IEnumerable<SelectListDto>> GetNames();
    }
}
