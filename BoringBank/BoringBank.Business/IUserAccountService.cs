using System.Collections.Generic;

namespace BoringBank.Business
{
    public interface IUserAccountService
    {
        IReadOnlyList<Domain.Account> GetAccountsForCustomer(int customerId);
        void RenameAccount(int customerId, int accountId, string newAccountName);
        void CreateAccountForCustomer(int userId, string accountName);
        void Transfer(int userId, int fromAccountId, int toAccountId, decimal amountToTransfer);
    }
}