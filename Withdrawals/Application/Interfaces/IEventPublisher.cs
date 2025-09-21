using Withdrawals.Domain.Events;

namespace Withdrawals.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync(WithdrawalEvent withdrawalEvent, CancellationToken cancellationToken = default);
    }
}
