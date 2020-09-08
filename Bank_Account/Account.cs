using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account
{
    abstract class Account
    {
        readonly public List<Person> users = new List<Person>();
        readonly public List<Transaction> transactions = new List<Transaction>();
        private static int Last_Number = 100000;
        public string Number { get; }
        public double Balance { get; protected set; }
        public double LowestBalance { get; protected set; }

        // The constructor
        public Account(string type, double balance)
        {
            Number = type + Last_Number;
            Last_Number += 1;
            Balance = balance;
            LowestBalance = balance;
        }

        // Deposit method when the person deposit money to the account
        public void Deposit(double amount, Person person)
        {
            // deposit the amount of money
            Balance += amount;

            // If the lowest balance is 0, make it same with the current balance
            if (LowestBalance == 0)
            {
                LowestBalance = Balance;
            }

            // After lowest balance becomes more than 0, if the balance is less than lowest balance, assign the balance to lowest balance to make it lowest
            if (LowestBalance > Balance)
            {
                LowestBalance = Balance;
            }

            // create a transaction object
            //-----------------------
            Transaction transaction = new Transaction(Number, amount, person, DateTime.Now);

            // add the object to the array
            transactions.Add(transaction);
        }

        // AddUser method adds Person object to users array
        public void AddUser(Person person)
        {
            users.Add(person);
        }

        // IsUsermethod returns bool value if there is any user that matches the name
        public bool IsUser(string name)
        {
            bool userNameExixts = false;

            // If there is matching user name in the list, it assigns true to userNameExists
            foreach (var user in users)
            {
                if (user.Name == name)
                {
                    userNameExixts = true;
                }
            }
            return userNameExixts;
        }

        // Abstract method
        public abstract void PrepareMonthlyReport();

        public override string ToString()
        {
            string usersArray = "";
            string transactionArray = "";
            foreach (var user in users)
            {
                usersArray += user + " ";
            }

            foreach (var transaction in transactions)
            {
                transactionArray += transaction + "\n";
            }
            string tableHeader = transactionArray == "" ? "\nNO TRANSACTION" : "\nTIME       NAME             ACCOUNT       TYPE      AMOUNT";
            return "--------------------------------------------------------------" + "\nNum: " + Number + "\n" + "User: " + usersArray + "\n" + tableHeader +
                "\n" + transactionArray + "\nCurrent Balance: $" + Balance + "\n" + "Lowest  Balance: $" + LowestBalance;
        }
    }
}
