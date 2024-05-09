using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace HMSMVC.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponseModel>> CreateAsync(RegistrationModel userRequestModel);
        Task<BaseResponse<UserResponseModel>> GetAsync(Guid id);
        Task<BaseResponse<ICollection<UserResponseModel>>> GetAllAsync();
        Task<BaseResponse<UserResponseModel>> UpdateAsync(Guid id, UserUpdateModel userUpdateModel);
        Task<BaseResponse<UserResponseModel>> LoginAsync(LoginModel loginRequestModel);
        Task<SignInResult> PasswordSignInAsync(LoginModel loginModel);
        Task<IdentityResult> CreateUserAsync(RegistrationModel registrationModel);

    }
}
