using System.Runtime.Serialization;
using System;
using System.Linq;
using BalanceReconciliation.Service.Reconcile;
using BalanceReconciliation.Test.Util;
using Xunit;
using BalanceReconciliation.Service.Reconcile.Dtos;
using Moq;
using Microsoft.Extensions.Logging;

namespace BalanceReconciliation.Test
{
    public class BalanceReconciliationServiceTest
    {
        [Fact]
        public void shoulReturnExpectedResponse()
        {
            int[] expected = {93,70,20,-152,-233,-228};
            var loggerMock = new Mock<ILogger<Reconcile>>();
            var serviceResponse = new Reconcile(loggerMock.Object);
            var json = new Helper().loadJson();
            var result = serviceResponse.performReconciliation(json);
            var res = (ReconcileResponseDto)result.Result.Data;
            var _ = res.EndOfDayBalances.Select(x => x.Balance).ToArray();
            Assert.Equal(expected, _);
        }

        [Fact]
        public void shoulReturnNullData_onException()
        {
            int[] expected = {93,70,20,-152,-233,-228};
            var loggerMock = new Mock<ILogger<Reconcile>>();
            var serviceResponse = new Reconcile(loggerMock.Object);
            var json = new Helper().loadJson();
            var result = serviceResponse.performReconciliation("");
            var res = (ReconcileResponseDto)result.Result.Data;
            Assert.Null(res);
        }
    }
}
