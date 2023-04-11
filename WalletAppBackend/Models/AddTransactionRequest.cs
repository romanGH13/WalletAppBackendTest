using System.ComponentModel.DataAnnotations;
using WalletAppBackend.Enums;

namespace WalletAppBackend.Models
{
    public record AddTransactionRequest : BaseRequest
    {
        [StringLength(256)]
        public required string Name { get; init; }

        [StringLength(2000)]
        public string? Description { get; init; }

        public required decimal Sum { get; init; }

        public required TransactionType Type { get; init; }

        public required TransactionStatus Status { get; init; }

        [Range(1, Int32.MaxValue)]
        public required int UserId { get; init; }

        [Range(1, Int32.MaxValue)]
        public int? AuthorizedUserId { get; init; }

    }

}
