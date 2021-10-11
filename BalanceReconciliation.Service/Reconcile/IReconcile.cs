using System.Threading.Tasks;
using BalanceReconciliation.Service.Communication;

namespace BalanceReconciliation.Service.Reconcile
{
    public interface IReconcile
    {
        Task<ServiceResponse> performReconciliation(string txn);
    }
}