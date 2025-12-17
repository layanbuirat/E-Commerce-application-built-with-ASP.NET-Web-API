using System.Text.Json.Serialization;
using SHOP.DAL.Models;

namespace SHOP.DAL.DTO.Requests
{
    public class CheckOutRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
