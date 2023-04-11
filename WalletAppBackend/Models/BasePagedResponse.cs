namespace WalletAppBackend.Models
{

    public abstract record BasePagedResponse : BaseResponse
    {
        public required int CurrentPage { get; init; }
        public required int PageCount { get; init; }
        public required int PageSize { get; init; }
        public required int RowCount { get; init; }
    }

}
