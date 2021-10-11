using System.Linq;
using System.Threading.Tasks;
using BalanceReconciliation.Service.Communication;
using BalanceReconciliation.Service.Reconcile.Dtos;
using Newtonsoft.Json;

namespace BalanceReconciliation.Service.Reconcile
{
    public class Reconcile : IReconcile
    {
        private readonly ServiceResponse serviceResponse;
        private readonly ReconcileResponseDto reconcileResponse;
        public Reconcile()
        {
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
                        Total = initBalance += y.Sum(y => y._amount)
                    })
                    .ToList();
                
                reconcileResponse.TotalCredits = txns.Where(i => i.creditDebitIndicator.Equals("Credit")).Count();
                reconcileResponse.TotalDebits = txns.Where(i => i.creditDebitIndicator.Equals("Debit")).Count();
                serviceResponse.Code = ErrorCodes.Success;
                serviceResponse.Data = reconcileResponse;
            }
            catch (System.Exception ex)
            {
                serviceResponse.Code = ErrorCodes.Exception;
                serviceResponse.Data = ex.Message;
            }

            return Task.FromResult(serviceResponse);
        }
    }
}