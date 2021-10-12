using System.Linq;
using System.Threading.Tasks;
using BalanceReconciliation.Service.Communication;
using BalanceReconciliation.Service.Reconcile.Dtos;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace BalanceReconciliation.Service.Reconcile
{
    public class Reconcile : IReconcile
    {
        private readonly ILogger _logger;
        private readonly ServiceResponse serviceResponse;
        private readonly ReconcileResponseDto reconcileResponse;
        public Reconcile(ILogger<Reconcile> logger)
        {
            _logger = logger;
            serviceResponse = new ServiceResponse();
            reconcileResponse = new ReconcileResponseDto();
        }

        public Task<ServiceResponse> performReconciliation(string txn)
        {
            try
            {
                var _txn = JsonConvert.DeserializeObject<ReconcileRequestDto>(txn);
                var balance = _txn.accounts[0].balances.current.amount;
                var initBalance = 0;
                var txns = _txn.accounts[0].transactions;
                var res = txns
                    .GroupBy(x => x.bookingDate)
                    .OrderBy(o => o.Key)
                    .Select(y => new {
                        Date = y.Key.ToString("yyyy-MM-dd"),
                        EODBalance = initBalance += y.Sum(y => y._amount),
                        TotalCredits = y.Count(i => i.creditDebitIndicator.ToLower().Equals("credit")),
                        TotalDebits = y.Count(i => i.creditDebitIndicator.ToLower().Equals("debit"))
                    })
                    .ToList();
                
                reconcileResponse.TotalCredits = res.Sum(x => x.TotalCredits);
                reconcileResponse.TotalDebits = res.Sum(x =>x.TotalDebits);
                reconcileResponse.EndOfDayBalances= res.Select(x => new DayBalance() {
                    Date = x.Date,
                    Balance = x.EODBalance
                }).ToList();

                serviceResponse.Code = ErrorCodes.Success;
                serviceResponse.Data = reconcileResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"performReconciliation(): {ex.ToString()}");
                serviceResponse.Code = ErrorCodes.Exception;
                serviceResponse.Message = ex.Message;
            }

            return Task.FromResult(serviceResponse);
        }
    }
}