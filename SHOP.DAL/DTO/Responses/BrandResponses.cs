using System.Text.Json.Serialization;


namespace SHOP.DAL.DTO.Responses
{
    public class BrandResponses
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string MainImage { get; set; }

        public string MainImageUrl => $"https://localhost:7254/images/{MainImage}";
    }
}
