using Dapper;
using System.Data;
using Withdrawals.Application.Interfaces;
using Withdrawals.Domain.Entities;

namespace Withdrawals.Infrustructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public AccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Account?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var sql = "SELECT Id, Balance FROM Accounts WHERE Id = @Id";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<Account>(sql, new { Id = accountId });
            return result;
        }

        public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            var sql = "UPDATE Accounts SET Balance = @Balance WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { account.Balance, account.Id });
        }
    }
}
