using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IGetDay
    {
        Task<IEnumerable<Day>> GetAsync();
        Task<Day> GetAsync(Guid id);
    }
}
