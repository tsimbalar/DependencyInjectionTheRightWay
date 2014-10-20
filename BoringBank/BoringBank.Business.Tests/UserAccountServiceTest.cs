using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoringBank.Business.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace BoringBank.Business.Tests
{
    [TestClass]
    public class UserAccountServiceTest
    {

        [TestMethod]
        public void RenameAccount_with_StateTesting_must_UpdateAccountName()
        {
            // Arrange		
            var newName = "someName";
            var existingAccount = AnAccount();

            var sut = new UserAccountService();
            sut.AccountRepository = //I want to put a fake here !

            // Act
            sut.RenameAccount(existingAccount.CustomerId, existingAccount.Id, newName);

            // Assert		
            // I want to verify what happened ..
        }

        private Account AnAccount()
        {
            var fixture = new Fixture();
            return fixture.Create<Account>();
        }
    }
}