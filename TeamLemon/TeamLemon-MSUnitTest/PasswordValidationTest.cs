using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamLemon.Controls;
using TeamLemon.Models;

namespace TeamLemon_MSUnitTest
{
    [TestClass]
    public class PasswordValidationTest
    {
        [TestMethod]
        public void ValidatePassword_CorrectPasswordTest()
        {
            // Arrange
            User user = new User()
            {
                Name = "user",
                Password = "password",
                IsAdmin = false,
                ID = 1,
                LogInAttempt = 3,
                LockedUser = false,
                SavingsAccounts = new List<Account>(),
                Changelog = new List<string>()
            };

            string psw = "password";

            // Act
            AccountManagement.ValidatePassword(user, psw);

            // Assert
            Assert.IsTrue(AccountManagement.ValidatePassword(user, psw));

        }

    }
}
