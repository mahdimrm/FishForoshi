using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IGetBarber
    {
        Task<IEnumerable<Barber>> GetAsync();
        Task<Barber> GetAsync(Guid? id);
    }
}
