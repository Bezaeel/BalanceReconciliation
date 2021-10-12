using System.Collections.Generic;
using System;
using System.Security.Cryptography.X509Certificates;
namespace BalanceReconciliation.Service.Reconcile.Dtos
{
    public class ReconcileRequestDto
    {
        public DateTime requestDateTime { get; set; }
        public List<Account> accounts { get; set; }
    }
    public class Current
    {
        public int amount { get; set; }
        public string creditDebitIndicator { get; set; }
        public object creditLines { get; set; }
    }

    public class Balances
    {
        public Current current { get; set; }
    }
    public class Account
    {
        public Balances balances { get; set; }
        public List<Transaction> transactions { get; set; }
    }
    public enum CreditDebitIndicator {
        Credit = 0,
        Debit
    }
    public class Transaction 
    {
        public int amount { get; set; }
        public string creditDebitIndicator { get; set; }
        public int _amount => creditDebitIndicator == "Credit" ? amount : -1 * amount;
        public DateTime bookingDate { get; set; }
        // public CreditDebitIndicator creditDebitIndicator { get; set; }
    }
    public class ReconcileResponseDto
    {
        public int TotalCredits { get; set; }
        public int TotalDebits { get; set; }
        public List<DayBalance> EndOfDayBalances { get; set; }
        
    }

    public class DayBalance
    {
        public string Date { get; set; }
        public int Balance { get; set; }
    }
}