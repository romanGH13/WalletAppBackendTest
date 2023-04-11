using WalletAppBackend.Entities;

namespace WalletAppBackend.Models
{
    public record PagedResponse<T> : BasePagedResponse where T : IEntity
    {
        public required IList<T> Results { get; init; }

        public PagedResponse()
        {
            Results = new List<T>();
        }
    }
}
