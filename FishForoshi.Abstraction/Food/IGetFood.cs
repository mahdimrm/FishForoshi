using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;
using FishForoshi.ViewModel.Common;

namespace FishForoshi.Abstraction
{
    public interface IGetFood
    {
        Task<IPagedList<Food>> GetFoodsAsync(int pageNumber, string name);

        Task<Food> GetByIdAsync(Guid id);

        Task<IEnumerable<SelectListDto>> GetBreakFastNames();
        Task<IEnumerable<SelectListDto>> GetEmployeeLaunchNames();
        Task<IEnumerable<SelectListDto>> GetSnackNames();
        Task<IEnumerable<SelectListDto>> GetSoldierLaunchNames();
        Task<IEnumerable<SelectListDto>> GetSoldierDinndersNames();
    }
}
