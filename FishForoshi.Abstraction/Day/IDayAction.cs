using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IDayAction
    {
        Task<DayActionStatus> CreateAsync(Day day);
        Task<DayActionStatus> UpdateAsync(Day day);
        Task<DayActionStatus> DeleteAsync(Guid id);
    }
}
