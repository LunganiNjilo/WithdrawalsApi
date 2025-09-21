using Withdrawals.Application.Interfaces;
using Withdrawals.Domain.Events;

namespace Withdrawals.Infrustructure.Messaging
{
    public class InMemoryEventPublisher : IEventPublisher
    {
        private readonly List<WithdrawalEvent> _events = new();

        public Task PublishAsync(WithdrawalEvent withdrawalEvent, CancellationToken cancellationToken = default)
        {
            _events.Add(withdrawalEvent);
            Console.WriteLine($"[Event Published] {withdrawalEvent}");
            return Task.CompletedTask;
        }

        public IReadOnlyList<WithdrawalEvent> Events => _events;
    }
}
