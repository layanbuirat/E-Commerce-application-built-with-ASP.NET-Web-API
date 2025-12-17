using Microsoft.AspNetCore.Http;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;

namespace SHOP.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest request);
        Task<string> ConfirmEmailAsync(string token, string userId);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);

    }
}
