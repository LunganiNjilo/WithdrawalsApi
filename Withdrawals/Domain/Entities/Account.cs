namespace Withdrawals.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public decimal Balance { get; private set; }

        public Account(Guid id, decimal balance)
        {
            Id = id;
            Balance = balance;
        }

        public bool CanWithdraw(decimal amount) => Balance >= amount;

        public void Withdraw(decimal amount)
        {
            if (!CanWithdraw(amount))
                throw new InvalidOperationException("Insufficient funds");

            Balance -= amount;
        }
    }
}
