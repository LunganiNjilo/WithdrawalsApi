using Withdrawals.Domain.Entities;
namespace Withdrawals.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
    }
}

