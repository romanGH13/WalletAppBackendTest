namespace WalletAppBackend.Models
{
    public record AddUserRequest
    {
        public required string UserName { get; init; }
    }
}
