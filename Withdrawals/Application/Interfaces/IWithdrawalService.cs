namespace Withdrawals.Application.Interfaces
{
    public interface IWithdrawalService
    {
        Task<bool> WithdrawAsync(Guid accountId, decimal amount, CancellationToken cancellationToken = default);
    }
}
