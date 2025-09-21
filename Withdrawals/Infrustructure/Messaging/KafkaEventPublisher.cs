using Confluent.Kafka;
using System.Text.Json;
using Withdrawals.Application.Interfaces;
using Withdrawals.Domain.Events;

namespace Withdrawals.Infrustructure.Messaging
{
    public class KafkaEventPublisher : IEventPublisher
    {
        private readonly IProducer<string, string> _producer;

        public KafkaEventPublisher(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(WithdrawalEvent withdrawalEventt, CancellationToken cancellationToken = default)
        {
            var topic = typeof(WithdrawalEvent).Name; // e.g. "WithdrawalEvent"
            var message = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(withdrawalEventt)
            };

            await _producer.ProduceAsync(topic, message, cancellationToken);
        }
    }
}
