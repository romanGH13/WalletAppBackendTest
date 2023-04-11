using WalletAppBackend.Entities;
using WalletAppBackend.Models;

namespace WalletAppBackend.Services
{
    public interface IUserService
    {
        Task<User> AddUser(string userName);

        PagedResponse<User> GetUsers(int page, int pageSize);

        Task<User?> GetById(int id);

        string CalculateDailyPoints();
    }
}
