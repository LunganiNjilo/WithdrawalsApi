namespace Withdrawals.Domain.Events
{
    public record WithdrawalEvent(Guid AccountId, decimal Amount, string Status);
}
