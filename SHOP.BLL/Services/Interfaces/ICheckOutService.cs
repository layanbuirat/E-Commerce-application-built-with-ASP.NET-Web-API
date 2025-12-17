using Microsoft.AspNetCore.Http;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;

namespace SHOP.BLL.Services.Interfaces
{
    public interface ICheckOutService
    {
        Task<CheckOutResponse> ProcsessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest);

        Task<bool> HandlePaymentSuccessAsync(int orderId);
    }
}
