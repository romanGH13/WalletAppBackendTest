namespace WalletAppBackend.Models
{
    public record AddTransactionResponse : BaseResponse
    {
        public required int Id { get; init; }
    }
}
