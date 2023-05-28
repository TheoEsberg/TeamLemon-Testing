using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamLemon.Controls;
using TeamLemon.Models;

namespace TeamLemon_MSUnitTest
{
    [TestClass]
    public class ExternalTransferTest
    {
        [TestMethod]
        public void MakeExternalTransfer_TransfersAmountBetweenAccountsWithSameCurrency()
        {
            // Arrange
            // Create two users
            User user1 = new User()
            {
                Name = "user1",
                Password = "password",
                IsAdmin = false,
                ID = 1,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            User user2 = new User()
            {
                Name = "user2",
                Password = "password",
                IsAdmin = false,
                ID = 2,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            // Create test accounts for the users
            List<Account> user1Accounts = new List<Account>()
            {
                new Account { AccountName = "Test Money: ", Balance = 50, AccountID = "123", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 200, AccountID = "456", Culture = new CultureInfo("sv-SE") }
            };
            List<Account> user2Accounts = new List<Account>()
            {
                new Account { AccountName = "Test Money: ", Balance = 50, AccountID = "321", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 100, AccountID = "654", Culture = new CultureInfo("sv-SE") }
            };
            
            // Add accounts to users
            Account.AllAccounts[user1.ID] = user1Accounts;
            Account.AllAccounts[user2.ID] = user2Accounts;
            Account.AllSavings.Add(user1.ID, user1.SavingsAccounts);
            Account.AllSavings.Add(user2.ID, user2.SavingsAccounts);

            decimal amount = 50;

            // Act 
            AccountManagement.MakeExternalTransfer(user1, user2.ID, amount, 1, "654");

            // Assert
            // Verify correct balance
            Assert.AreEqual(150, Account.AllAccounts[user1.ID][1].Balance);
            Assert.AreEqual(150, Account.AllAccounts[user2.ID][1].Balance);
        }

        [TestMethod]
        public void MakeExternalTransfer_TransfersAmountBetweenAccountsWithDifferentCurrencyUStoSE()
        {
            // Arrange
            // Create two users
            User user1 = new User()
            {
                Name = "user1",
                Password = "password",
                IsAdmin = false,
                ID = 3,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            User user2 = new User()
            {
                Name = "user2",
                Password = "password",
                IsAdmin = false,
                ID = 4,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            // Create test accounts for the users
            List<Account> user1Accounts = new List<Account>()
            {
                new Account { AccountName = "Test Money: ", Balance = 50, AccountID = "4", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 200, AccountID = "5", Culture = new CultureInfo("en-US") }
            };
            List<Account> user2Accounts = new List<Account>()
            {
                new Account { AccountName = "Test Money: ", Balance = 50, AccountID = "6", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 100, AccountID = "7", Culture = new CultureInfo("sv-SE") }
            };

            // Add accounts to users
            Account.AllAccounts[user1.ID] = user1Accounts;
            Account.AllAccounts[user2.ID] = user2Accounts;
            Account.AllSavings.Add(user1.ID, user1.SavingsAccounts);
            Account.AllSavings.Add(user2.ID, user2.SavingsAccounts);

            // Act 
            decimal amount = 50;
            AccountManagement.MakeExternalTransfer(user1, user2.ID, amount, 1, "7");
            amount = amount / Admin.usdValue;

            // Assert
            // Verify correct balance
            Assert.AreEqual(150, Account.AllAccounts[user1.ID][1].Balance);
            Assert.AreEqual(100 + amount, Account.AllAccounts[user2.ID][1].Balance);
        }

        [TestMethod]
        public void MakeExternalTransfer_TransfersAmountBetweenAccountsWithDifferentCurrencySEtoUS()
        {
            // Arrange
            // Create two users
            User user1 = new User()
            {
                Name = "user1",
                Password = "password",
                IsAdmin = false,
                ID = 5,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            User user2 = new User()
            {
                Name = "user2",
                Password = "password",
                IsAdmin = false,
                ID = 6,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            // Create test accounts for the users
            List<Account> user1Accounts = new List<Account>()
            {
                new Account { AccountName = "Test Money: ", Balance = 50, AccountID = "8", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 200, AccountID = "9", Culture = new CultureInfo("en-US") }
            };
            List<Account> user2Accounts = new List<Account>()
            {
                new Account { AccountName = "Test Money: ", Balance = 50, AccountID = "10", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 100, AccountID = "11", Culture = new CultureInfo("sv-SE") }
            };

            // Add accounts to users
            Account.AllAccounts[user1.ID] = user1Accounts;
            Account.AllAccounts[user2.ID] = user2Accounts;
            Account.AllSavings.Add(user1.ID, user1.SavingsAccounts);
            Account.AllSavings.Add(user2.ID, user2.SavingsAccounts);

            // Act 
            decimal amount = 50;
            AccountManagement.MakeExternalTransfer(user2, user1.ID, amount, 1, "9");
            amount = amount * Admin.usdValue;

            // Assert
            // Verify correct balance
            Assert.AreEqual(200 + amount, Account.AllAccounts[user1.ID][1].Balance);
            Assert.AreEqual(50, Account.AllAccounts[user2.ID][1].Balance);
        }
    }
}
