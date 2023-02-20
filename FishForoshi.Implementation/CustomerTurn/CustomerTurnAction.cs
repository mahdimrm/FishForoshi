using FishForoshi.Abstraction;
using FishForoshi.Entities;

namespace FishForoshi.Implementation
{
    public class CustomerTurnAction : ICustomerTurnAction
    {
        private readonly IQueryRepository<CustomerTurn> _query;
        private readonly ICudRepository<CustomerTurn> _action;

        public CustomerTurnAction
            (IQueryRepository<CustomerTurn> query,
            ICudRepository<CustomerTurn> action)
        {
            _query = query;
            _action = action;
        }

        public async Task<CustomerTurnActionStatus> CreateAsync(CustomerTurn turn)
        {
            return await _action.AddAsync(turn) ? CustomerTurnActionStatus.Success : CustomerTurnActionStatus.Failed;
        }

        public async Task<CustomerTurnActionStatus> DeleteAsync(Guid id)
            => await _action.DeleteByIdAsync(id)
                                    ? CustomerTurnActionStatus.Success
                                    : CustomerTurnActionStatus.Failed;

        public async Task<CustomerTurnActionStatus> UpdateAsync(CustomerTurn turn)
        {
            var model = await _query.GetAsync(turn.Id);

            model.CustomerName = turn.CustomerName;
            model.CustomerQueueTime = turn.CustomerQueueTime;
            model.BarberStaffId = turn.BarberStaffId;

            return await _action.UpdateAsync(model)
                ? CustomerTurnActionStatus.Success : CustomerTurnActionStatus.Failed;
        }

        public async Task<CustomerTurnActionStatus> UpsertAsync(CustomerTurn turn)
            => turn?.Id is null ? await CreateAsync(turn) : await UpdateAsync(turn);
    }
}
