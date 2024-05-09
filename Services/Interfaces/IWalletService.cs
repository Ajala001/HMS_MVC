using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IWalletService
    {
        Task<BaseResponse<WalletResponseModel>> CreateAsync(string email);
        Task<BaseResponse<WalletResponseModel>> UpdateAsync(WalletUpdateModel walletUpdateModel);
        Task<BaseResponse<WalletResponseModel>> GetAsync(Guid walletId);
    }
}
