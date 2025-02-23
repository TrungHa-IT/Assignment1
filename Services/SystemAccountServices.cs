using ASS1.Models;
using ASS1.Repositories;

namespace ASS1.Services
{
    public class SystemAccountServices : ISystemAccountServices
    {
        private readonly ISystemAccountRepository _systemAccountRepository;
        public SystemAccountServices(ISystemAccountRepository systemAccountRepository)
        {
            _systemAccountRepository = systemAccountRepository;
        }
        public async Task AddAccount(SystemAccount account)
        {
            await _systemAccountRepository.AddAccount(account);
        }

        public async Task DeleteAccount(int accountId)
        {
            await _systemAccountRepository.DeleteAccount(accountId);
        }

        public async Task<SystemAccount?> GetAccountByEmail(string email)
        {
            return await _systemAccountRepository.GetAccountByEmail(email);
        }

        public async Task<SystemAccount?> GetAccountById(short accountId)
        {
            return await _systemAccountRepository.GetAccountById(accountId);
        }

        public async Task<IEnumerable<SystemAccount>> GetAllSystemAccounts()
        {
            return await _systemAccountRepository.GetAllSystemAccounts();
        }

        public async Task UpdateAccount(SystemAccount account)
        {
            await _systemAccountRepository.UpdateAccount(account);
        }
    }
}
