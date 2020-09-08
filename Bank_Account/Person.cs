using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account
{
    class Person
    {
        private string password;

        public bool IsAuthenticated { get; private set; }
        public string SIN { get; }
        public string Name { get; }

        // The constructor
        public Person(string name, string sin)
        {
            Name = name;
            SIN = sin;
            password = SIN.Substring(0, 3);
        }

        public void Login(string password)
        {
            // If the password is correct, make the user's ID authenticated
            if (password == this.password)
            {
                IsAuthenticated = true;
            }
            else // Otherwise, make the user's ID not authenticated and throw an exception
            {
                IsAuthenticated = false;
                throw new AccountException(ExceptionEnum.PASSWORD_INCORRECT);
            }
        }

        // make the user log out by assigning false to IsAuthenticated
        public void Logout()
        {
            IsAuthenticated = false;
        }

        public override string ToString()
        {
            string status = (IsAuthenticated == true) ? "LogIn" : "LogOut";
            return $"{Name}({status})";
        }
    }
}
