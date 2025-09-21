using Microsoft.AspNetCore.Mvc;
using Withdrawals.Application.Interfaces;

namespace Withdrawals.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : Controller
    {
        private readonly IWithdrawalService _withdrawalService;
        public BankAccountController(IWithdrawalService withdrawalService)
        {
            _withdrawalService = withdrawalService;
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(Guid accountId, decimal amount, CancellationToken cancellationToken)
        {
            var success = await _withdrawalService.WithdrawAsync(accountId, amount, cancellationToken);

            return success
                ? Ok("Withdrawal successful")
                : BadRequest("Withdrawal failed or insufficient funds");
        }
    }
}
