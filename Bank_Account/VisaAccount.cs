using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account
{
    class VisaAccount : Account, ITransaction
    {
        private double creditLimit;
        private static double INTEREST_RATE = 0.1995;

        // The constructor
        public VisaAccount(double balance = 0, double creditLimit = 1200) : base("VS-", balance)
        {
            this.creditLimit = creditLimit;
        }

        // The withdraw method (from ITransaction)
        public void Withdraw(double amount, Person person) { }

        // DoPayment method when the person deposit money to the account
        public void DoPayment(double amount, Person person)
        {
            Deposit(amount, person);
        }

        // DoPurchase method when the person spend money from the account
        public void DoPurchase(double amount, Person person)
        {
            // if there is matching name in the account, increment matchingName by 1
            int matchingName = 0;

            foreach (var user in users)
            {
                if (user.Name == person.Name)
                {
                    matchingName++;
                }
            }

            // If the matchingName Value is still 0 without any increment, it throws an exception
            if (matchingName <= 0)
            {
                throw new AccountException(ExceptionEnum.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            // If the value of IsAuthenticated is false, it throws an exception
            if (person.IsAuthenticated == false)
            {
                throw new AccountException(ExceptionEnum.USER_NOT_LOGGED_IN);
            }

            // If the amount of purchase is greater than credit Limit, it throws an exception
            else if (amount > creditLimit)
            {
                throw new AccountException(ExceptionEnum.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
            }

            // If it does not have any conditions above, it invokes Deposit method when the person purchase something
            else
            {
                Deposit(-amount, person);
            }
        }

        public override void PrepareMonthlyReport()
        {
            // It subtracts monthly interest from the balance
            double interest = LowestBalance * INTEREST_RATE / 12d;
            Balance -= interest;
            // It clears the transaction history
            transactions.Clear();
        }
    }
}
