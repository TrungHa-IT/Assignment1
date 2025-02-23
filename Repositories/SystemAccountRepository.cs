using ASS1.DAO;
using ASS1.Models;

namespace ASS1.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly ISystemAccountDAO _systemAccountDAO;
        public SystemAccountRepository(ISystemAccountDAO systemAccountDAO)
        {
            _systemAccountDAO = systemAccountDAO;
        }
        public async Task AddAccount(SystemAccount account)
        {
            await _systemAccountDAO.AddAccount(account);
        }

        public async Task DeleteAccount(int accountId)
        {
            await _systemAccountDAO.DeleteAccount(accountId);
        }

        public async Task<SystemAccount?> GetAccountByEmail(string email)
        {
            return await _systemAccountDAO.GetAccountByEmail(email);
        }

        public async Task<SystemAccount?> GetAccountById(short accountId)
        {
            return await _systemAccountDAO.GetAccountById(accountId);
        }

        public async Task<IEnumerable<SystemAccount>> GetAllSystemAccounts()
        {
            return await _systemAccountDAO.GetAllSystemAccounts();
        }

        public async Task UpdateAccount(SystemAccount account)
        {
            await _systemAccountDAO.UpdateAccount(account);
        }
    }
}
