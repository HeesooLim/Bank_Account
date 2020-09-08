using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account
{
    class CheckingAccount : Account, ITransaction
    {
        private static double COST_PER_TRANSACTION = 0.05;
        private static double INTEREST_RATE = 0.005;
        private bool hasOverdraft;

        // The constructor
        public CheckingAccount(double balance = 0, bool hasOverdraft = false) : base("CK-", balance)
        {
            this.hasOverdraft = hasOverdraft;
        }

        // Deposit method that deposits money to the account
        public new void Deposit(double amount, Person person)
        {
            base.Deposit(amount, person);
        }

        // Withdraw method that withdraws money from the account
        public void Withdraw(double amount, Person person)
        {
            int matchingName = 0;

            // If the user name is matching the name of the owner of the account, add 1 to matchingName
            foreach (var user in users)
            {
                if (user.Name == person.Name)
                {
                    matchingName++;
                }
            }

            // If matching Name variable is 0, which means there was no matching name, throw an exception
            if (matchingName <= 0)
            {
                throw new AccountException(ExceptionEnum.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            // if the value of IsAuthenticated is false, throw an exception
            if (person.IsAuthenticated == false)
            {
                throw new AccountException(ExceptionEnum.USER_NOT_LOGGED_IN);
            }

            // If Withdraw amount is greater than balance and the value of hasOverdraft is false, throw an exception
            else if (amount > Balance && hasOverdraft == false)
            {
                throw new AccountException(ExceptionEnum.NO_OVERDRAFT);
            }

            // If above conditions are not applied, invoke Deposit method
            else
            {
                Deposit(-amount, person);
            }
        }

        public override void PrepareMonthlyReport()
        {
            // Change the balance by adding monthly interest and subtracting transaction cost
            double totalTransaction = COST_PER_TRANSACTION * transactions.Count();
            double interest = LowestBalance * INTEREST_RATE / 12d;
            Balance = Balance + interest - totalTransaction;
            // Clear the transaction history
            transactions.Clear();
        }
    }
}
