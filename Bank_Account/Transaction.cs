using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account
{
    class Transaction
    {
        public string AccountNumber { get; }
        public double Amount { get; }
        public Person Originator { get; }
        public DateTime Time { get; }

        // The constructor
        public Transaction(string accountNumber, double amount, Person person, DateTime time)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            Originator = person;
            Time = time;
        }

        public override string ToString()
        {
            // If the amount of money is positive, It assigns Deposit to transaction type
            string transactionType = Amount > 0 ? "Deposit" : "Withdraw";

            // Remove '-' sign from withdraw amount that is used for calculating
            double typeAmount = Amount > 0 ? Amount : -Amount;
            return $"[{Time.ToShortTimeString()}] {Originator,-17}NO.{AccountNumber}  {transactionType,-9} ${typeAmount}";
        }
    }
}
