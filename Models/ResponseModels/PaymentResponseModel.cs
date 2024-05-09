namespace HMSMVC.Models.ResponseModels
{
    public class PaymentResponseModel
    {
        public Guid Id { get; set; }
        public Guid RefNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
        
        public string PatientCode { get; set; } = default!;
        public string PaidFor { get; set; } = default!;
        public DateTime PaidOn { get; set; }

    }
}
