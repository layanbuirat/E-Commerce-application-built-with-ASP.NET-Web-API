using System.Text.Json.Serialization;


namespace SHOP.DAL.DTO.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public string MainImage { get; set; }

        public string MainImageUrl { get; set; }

        public List<string> SubImagesUrls { get; set; } = new List<string>();

        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
    }
}
