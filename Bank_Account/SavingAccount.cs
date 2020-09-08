using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account
{
    class SavingAccount : Account, ITransaction
    {
        private static double COST_PER_TRANSACTION = 0.05;
        private static double INTEREST_RATE = 0.015;

        // Constructor (Invokes the base constructor with "SV-")
        public SavingAccount(double balance = 0) : base("SV-", balance)
        {

        }

        // Deposit method deposits the amount of money to the account
        public new void Deposit(double amount, Person person)
        {
            base.Deposit(amount, person);
        }

        // Withdraw method withdraws the amount of money from the account
        public void Withdraw(double amount, Person person)
        {
            int matchingName = 0;

            // if there is matching name among account users, increment matchingName by 1
            foreach (var user in users)
            {
                if (user.Name == person.Name)
                {
                    matchingName++;
                }
            }

            // If matchingName is not incremented and is still 0, it throws an exception
            if (matchingName <= 0)
            {
                throw new AccountException(ExceptionEnum.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            // If the value of IsAuthenticated is false, it throws an exception
            if (person.IsAuthenticated == false)
            {
                throw new AccountException(ExceptionEnum.USER_NOT_LOGGED_IN);
            }

            // If the amount of money that will be withdrawn is greater than the balance, it throws an exception
            else if (amount > Balance)
            {
                throw new AccountException(ExceptionEnum.NO_OVERDRAFT);
            }

            // If there was no condition that is applied, it invokes the Deposit method to withdraw money from the account
            else
            {
                Deposit(-amount, person);
            }
        }

        public override void PrepareMonthlyReport()
        {
            // It adds monthly interest and subtracts total transaction cost
            double totalTransaction = COST_PER_TRANSACTION * transactions.Count();
            double interest = LowestBalance * INTEREST_RATE / 12d;
            Balance = Balance + interest - totalTransaction;
            // It clears the transaction history
            transactions.Clear();
        }
    }
}
