using WalletAppBackend.Entities;
using WalletAppBackend.Enums;
using WalletAppBackend.Models;

namespace WalletAppBackend.Services
{
    public interface ITransactionService
    {
        Task<Transaction> AddTransaction(string name, TransactionStatus status, TransactionType type, decimal sum, int userId, string? description = null, int? authorizedUserId = null);

        Transaction? GetTransactionById(int id);

        PagedResponse<Transaction> GetTransactionsByUserId(int userId, int page, int pageSize);

        void DeleteTransaction(Transaction transaction);
    }
}
