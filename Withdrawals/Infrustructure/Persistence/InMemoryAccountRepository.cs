using Withdrawals.Application.Interfaces;
using Withdrawals.Domain.Entities;

namespace Withdrawals.Infrustructure.Persistence
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly Dictionary<Guid, Account> _accounts = new();

        public InMemoryAccountRepository()
        {
            // Seed with test account
            var accountId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            _accounts[accountId] = new Account(accountId, 1000m); // balance 1000
        }

        public Task<Account?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            _accounts.TryGetValue(accountId, out var account);
            return Task.FromResult(account);
        }

    
        public Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            _accounts[account.Id] = account;
            return Task.CompletedTask;
        }
    }
}
