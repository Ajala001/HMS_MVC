namespace HMSMVC.Models.ResponseModels
{
    public class WalletResponseModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public decimal Balance { get; set; } 
    }
}
