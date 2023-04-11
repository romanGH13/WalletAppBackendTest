using System.ComponentModel.DataAnnotations.Schema;
using WalletAppBackend.Enums;

namespace WalletAppBackend.Entities
{
    public record Transaction : IEntity
    {
        public required string Name { get; init; }

        public string? Description { get; init; }

        public required decimal Sum { get; init; }

        public required TransactionType Type { get; init; }

        public required TransactionStatus Status { get; init; }

        [ForeignKey("User")]
        public required int UserId { get; init; }

        public User User { get; init; }


        [ForeignKey("AuthorizedUserId")]
        public int? AuthorizedUserId { get; init; }

        public User? AuthorizedUser { get; init; }
    }
}
