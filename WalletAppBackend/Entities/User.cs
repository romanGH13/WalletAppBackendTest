using System.ComponentModel.DataAnnotations;

namespace WalletAppBackend.Entities
{
    public record User : IEntity
    {
        [Required]
        public required string UserName { get; init; }

        public required decimal CardBalance { get; init; }

        public virtual ICollection<Transaction>? Transactions { get; init; }

        public virtual ICollection<Transaction>? AuthorizedTransactions { get; init; }

    }
}
