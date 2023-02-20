using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation;

public class BarberAction : IBarberAction
{
    private readonly ICudRepository<Barber> _action;
    private readonly IQueryRepository<Barber> _query;

    public BarberAction(ICudRepository<Barber> action, IQueryRepository<Barber> query)
    {
        _action = action;
        _query = query;
    }

    public async Task<BarberActionStatus> CreateAsync(Barber barber)
    {
        return await _action.AddAsync(barber) ? BarberActionStatus.Success : BarberActionStatus.Failed;
    }

    public async Task<BarberActionStatus> DeleteAsync(Guid id)
        => await _action.DeleteByIdAsync(id)
                                ? BarberActionStatus.Success
                                : BarberActionStatus.Failed;

    public async Task<BarberActionStatus> UpdateAsync(Barber barber)
    {
        var model = await _query.GetAsync(barber.Id);

        model.FullName = barber.FullName;
        model.Type = barber.Type;

        return await _action.UpdateAsync(model)
            ? BarberActionStatus.Success : BarberActionStatus.Failed;
    }

    public async Task<BarberActionStatus> UpsertAsync(Barber barber)
        => barber?.Id is null ? await CreateAsync(barber) : await UpdateAsync(barber);
}


