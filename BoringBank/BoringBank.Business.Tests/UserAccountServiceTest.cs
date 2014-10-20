using BoringBank.Business.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace BoringBank.Business.Tests
{
    [TestClass]
    public class UserAccountServiceTest
    {

        [TestMethod]
        public void RenameAccount_must_UpdateAccountName()
        {
            // Arrange		
            var newName = "someName";

            var existingAccount = AnAccount();
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(r => r.GetAccountForCustomer(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(existingAccount);
            var sut = new UserAccountService(mockRepo.Object);

            // Act
            sut.RenameAccount(existingAccount.CustomerId, existingAccount.Id, newName);

            // Assert		
            mockRepo.Verify(r => r.Update(It.Is<Account>(a => a.Name == newName)));
        }

        private Account AnAccount()
        {
            var fixture = new Fixture();
            return fixture.Create<Account>();
        }
    }
}