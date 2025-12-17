namespace SHOP.DAL.DTO.Responses
{
    public class CheckOutResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string? Url { get; set; }

        public string? PaymentId { get; set; }
    }
}
