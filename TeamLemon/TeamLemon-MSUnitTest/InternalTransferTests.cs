using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Globalization;
using TeamLemon.Controls;
using TeamLemon.Models;

namespace TeamLemon_MSUnitTest
{
    [TestClass]
    public class InternalTransferTests
    {
        [TestMethod]
        public void InternalTransfer_TransfersAmountBetweenAccounts()
        {
            // Arrange
            User currentUser = new User()
            {
                Name = "test",
                Password = "password",
                IsAdmin = false,
                ID = 1,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            // Create test accounts
            List<Account> accounts = new List<Account>()
            {
                new Account { AccountName = "TestMoney: ", Balance = 100, AccountID = "123", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 200, AccountID = "456", Culture = new CultureInfo("sv-SE") }
            };
            Account.AllAccounts[currentUser.ID] = accounts;

            // Set the Console input for the user's choises
            SetConsoleInput(new string[] { "1", "2", "50", "password" });

            // Act
            AccountManagement.InternalTransfer(currentUser);

            // Assert
            // Verify balance is correct
            Assert.AreEqual(50, Account.AllAccounts[currentUser.ID][0].Balance);
            Assert.AreEqual(250, Account.AllAccounts[currentUser.ID][1].Balance);

            // Verify transaction is logged
            Assert.AreEqual(1, currentUser.Changelog.Count);
            Assert.IsTrue(currentUser.Changelog[0].Contains("Transfered 50 to account '456' from account '123'"));
        }

        [TestMethod]
        public void InternalTransfer_TransfersAmountBetweenAccountsWithDifferentCurrenciesUStoSE()
        {
            // Arrange
            User currentUser = new User()
            {
                Name = "test",
                Password = "password",
                IsAdmin = false,
                ID = 2,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            // Create test accounts
            List<Account> accounts = new List<Account>()
            {
                new Account { AccountName = "TestMoney: ", Balance = 100, AccountID = "1", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 200, AccountID = "2", Culture = new CultureInfo("en-US") }
            };
            Account.AllAccounts[currentUser.ID] = accounts;
            decimal amount = 50;

            // Set the Console input for the user's choises
            SetConsoleInput(new string[] { "2", "1", $"{amount}", "password" });

            // Act
            AccountManagement.InternalTransfer(currentUser);
            amount = amount / Admin.usdValue;

            // Assert
            // Verify balance is correct
            Assert.AreEqual(100 + amount, Account.AllAccounts[currentUser.ID][0].Balance);
            Assert.AreEqual(150, Account.AllAccounts[currentUser.ID][1].Balance);

            // Verify transaction is logged
            Assert.AreEqual(1, currentUser.Changelog.Count);
            Assert.IsTrue(currentUser.Changelog[0].Contains($"Transfered {amount} to account '1' from account '2'"));
        }

        [TestMethod]
        public void InternalTransfer_TransfersAmountBetweenAccountsWithDifferentCurrenciesSEtoUS()
        {
            // Arrange
            User currentUser = new User()
            {
                Name = "test",
                Password = "password",
                IsAdmin = false,
                ID = 3,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            // Create test accounts
            List<Account> accounts = new List<Account>()
            {
                new Account { AccountName = "TestMoney: ", Balance = 100, AccountID = "3", Culture = new CultureInfo("sv-SE") },
                new Account { AccountName = "Salery: ", Balance = 200, AccountID = "4", Culture = new CultureInfo("en-US") }
            };
            Account.AllAccounts[currentUser.ID] = accounts;
            decimal amount = 50;

            // Set the Console input for the user's choises
            SetConsoleInput(new string[] { "1", "2", $"{amount}", "password" });

            // Act
            AccountManagement.InternalTransfer(currentUser);
            amount = amount * Admin.usdValue;

            // Assert
            // Verify balance is correct
            Assert.AreEqual(50, Account.AllAccounts[currentUser.ID][0].Balance);
            Assert.AreEqual(200 + amount, Account.AllAccounts[currentUser.ID][1].Balance);

            // Verify transaction is logged
            Assert.AreEqual(1, currentUser.Changelog.Count);
            Assert.IsTrue(currentUser.Changelog[0].Contains($"Transfered {amount} to account '4' from account '3'"));
        }

        private static void SetConsoleInput(string[] inputLines)
        {
            var input = string.Join(Environment.NewLine, inputLines);
            var stringReader = new System.IO.StringReader(input);
            Console.SetIn(stringReader);
        }
    }
}