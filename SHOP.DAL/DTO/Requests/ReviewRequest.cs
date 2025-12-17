namespace SHOP.DAL.DTO.Requests
{
    public class ReviewRequest
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
    }
}
