namespace Entities.DTOs.OrderDtos
{
    public class PaymentRequest
    {
        public int OrderId { get; set; }
        public string? CardNumber { get; set; }
        public string? CardHolder { get; set; }
        public string? Expiry { get; set; }
        public string? Cvc { get; set; }
        // İhtiyaca göre ek alanlar ekleyebilirsin
    }
}
