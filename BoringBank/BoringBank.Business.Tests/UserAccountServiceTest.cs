using BoringBank.Data;
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
            var sut = new UserAccountService();
            sut.AccountRepository = mockRepo.Object; //I want to put a fake here !

            // Act
            sut.RenameAccount(existingAccount.CustomerId, existingAccount.Id, newName);

            // Assert		
            mockRepo.Verify(r=> r.Update(It.Is<Data.Account>(a=> a.Title == newName)));
        }

        private Data.Account AnAccount()
        {
            var fixture = new Fixture();
            return fixture.Create<Data.Account>();
        }
    }
}