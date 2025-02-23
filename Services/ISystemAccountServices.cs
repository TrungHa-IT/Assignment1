using ASS1.Models;

namespace ASS1.Services
{
    public interface ISystemAccountServices
    {
        Task<IEnumerable<SystemAccount>> GetAllSystemAccounts();
        Task<SystemAccount?> GetAccountById(short accountId);
        Task<SystemAccount?> GetAccountByEmail(string email);
        Task AddAccount(SystemAccount account);
        Task UpdateAccount(SystemAccount account);
        Task DeleteAccount(int accountId);
    }
}
