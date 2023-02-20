using FishForoshi.Entities;

namespace FishForoshi.Abstraction
{
    public interface IBarberAction
    {
        Task<BarberActionStatus> CreateAsync(Barber barber);
        Task<BarberActionStatus> UpdateAsync(Barber barber);
        Task<BarberActionStatus> UpsertAsync(Barber barber);
        Task<BarberActionStatus> DeleteAsync(Guid id);
    }
}
