namespace SHOP.DAL.DTO.Responses
{
    public class CartSummaryResponse
    {
        public List<CartResponse> Items { get; set; } = new List<CartResponse>();
        public decimal CartTotal => Items.Sum(i => i.TotalPrice);
    }
}
