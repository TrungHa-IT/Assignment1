using ASS1.Models;


namespace ASS1.DAO
{
    public class SystemAccountDAO : ISystemAccountDAO
    {

        private readonly FunewsManagementContext _context;

        public SystemAccountDAO(FunewsManagementContext context)
        {
            _context = context;
        }
        public async Task AddAccount(SystemAccount account)
        {
            await _context.SystemAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int accountId)
        {
            var account = await _context.SystemAccounts.FindAsync(accountId);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public Task<SystemAccount?> GetAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<SystemAccount?> GetAccountById(short accountId)
        {
            var account = await _context.SystemAccounts.FindAsync(accountId);
            return account;
        }

        public Task<IEnumerable<SystemAccount>> GetAllSystemAccounts()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAccount(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
