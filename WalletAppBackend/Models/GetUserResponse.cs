namespace WalletAppBackend.Models
{
    public record GetUserResponse : BaseResponse
    {
        public required int Id { get; init; }

        public required string UserName { get; init; }

        public required decimal CardBalance { get; init; }

        public required decimal AvailableBalance { get; init; }

        public required string PaymentDue { get; init; }

        public required string DailyPoints { get; init; }
    }
}
