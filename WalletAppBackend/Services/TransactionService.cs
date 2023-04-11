using WalletAppBackend.Entities;
using WalletAppBackend.Enums;
using WalletAppBackend.Models;
using WalletAppBackend.Repositories;

namespace WalletAppBackend.Services
{
    public class TransactionService : ITransactionService
    {
        private IRepository<Transaction> _transactionRepository;

        public TransactionService(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Transaction? GetTransactionById(int id)
        {
            Transaction? transaction = _transactionRepository.GetQueryable().Where(x => x.Id == id).SingleOrDefault();

            return transaction;
        }

        public PagedResponse<Transaction> GetTransactionsByUserId(int userId, int page, int pageSize)
        {
            var query = _transactionRepository.GetQueryable().Where(x => x.UserId == userId);

            var result = _transactionRepository.GetPaged(query, page, pageSize);

            return result;
        }

        public async Task<Transaction> AddTransaction(string name, TransactionStatus status, TransactionType type, decimal sum, int userId, string description = "", int? authorizedUserId = null)
        {
            Transaction transaction = new Transaction()
            {
                Name = name,
                Status = status,
                Type = type,
                Sum = sum,
                Description = description,
                UserId = userId,
                AuthorizedUserId = authorizedUserId
            };

            _transactionRepository.Add(transaction);

            await _transactionRepository.SaveChangesAsync();

            return transaction;
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _transactionRepository.Delete(transaction);
        }
    }
}
