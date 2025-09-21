using Withdrawals.Application.Interfaces;
using Withdrawals.Domain.Events;

namespace Withdrawals.Application.Services
{
    public class WithdrawalService : IWithdrawalService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventPublisher _eventPublisher;

        public WithdrawalService(IAccountRepository accountRepository, IEventPublisher eventPublisher)
        {
            _accountRepository = accountRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<bool> WithdrawAsync(Guid accountId, decimal amount, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
            if (account is null)
            {
                await _eventPublisher.PublishAsync(new WithdrawalEvent(accountId, amount, "FAILED"), cancellationToken);
                return false;
            }

            if (!account.CanWithdraw(amount))
            {
                await _eventPublisher.PublishAsync(new WithdrawalEvent(accountId, amount, "FAILED"), cancellationToken);
                return false;
            }

            account.Withdraw(amount);
            await _accountRepository.UpdateAsync(account, cancellationToken);

            await _eventPublisher.PublishAsync(new WithdrawalEvent(accountId, amount, "SUCCESSFUL"), cancellationToken);
            return true;
        }
    }
}
