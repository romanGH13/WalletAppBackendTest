namespace WalletAppBackend.Models
{
    public record AddUserResponse : BaseResponse
    {
        public required int Id { get; init; }
    }
}
