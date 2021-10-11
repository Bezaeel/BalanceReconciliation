using System.Runtime.Serialization;
using System;
using BalanceReconciliation.Service.Reconcile;
using BalanceReconciliation.Test.Util;
using Xunit;

namespace BalanceReconciliation.Test
{
    public class BalanceReconciliationServiceTest
    {
        [Fact]
        public void shoulReturnExpectedResponse()
        {
            var serviceResponse = new Reconcile();
            var json = new Helper().loadJson();
            var result = serviceResponse.performReconciliation(json);
            Assert.Equal(result.Result.Code, Service.Communication.ErrorCodes.Success);
        }
    }
}
