namespace SHOP.DAL.Models
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }

        public string MainImage { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

    }
}
