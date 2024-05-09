using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;

namespace HMSMVC.Services.Implementations
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IPatientRepository _petientRepository;
        private readonly IUnitOfWork _unitOfWork;
        public WalletService(IWalletRepository walletRepository, IUnitOfWork unitOfWork, IPatientRepository patientRepository)
        {
            _walletRepository = walletRepository;
            _petientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<WalletResponseModel>> CreateAsync(string email)
        {
            var patient = await _petientRepository.GetAsync(p => p.User.Email == email);
            if(patient == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    IsSuccessful = false,
                    Message = "You dont have the role of a patient",
                    Data = null
                };
            }

            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Patient = patient,
                PatientId = patient.Id,
                Balance = 0.0M
            };

            return new BaseResponse<WalletResponseModel>
            {
                IsSuccessful = true,
                Message = "Wallet Created Successfully",
                Data = new WalletResponseModel
                {
                    Id = wallet.Id,
                    FullName = $"{patient.User.FirstName} {patient.User.LastName}",
                    Balance = wallet.Balance
                }
            };
        }

        public async Task<BaseResponse<WalletResponseModel>> GetAsync(Guid walletId)
        {
            var wallet = await _walletRepository.GetAsync(w => w.Id == walletId);
            if(wallet == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Wallet Not Found",
                    Data = null
                };
            }

            return new BaseResponse<WalletResponseModel>
            {
                IsSuccessful = true,
                Message = "Wallet Found",
                Data = new WalletResponseModel
                {
                    Id = wallet.Id,
                    FullName = $"{wallet.Patient.User.FirstName} {wallet.Patient.User.FirstName}",
                    Balance = wallet.Balance
                }
            };
        }

        public async Task<BaseResponse<WalletResponseModel>> UpdateAsync(WalletUpdateModel walletUpdateModel)
        {
            var wallet = await _walletRepository.GetAsync(w => w.Id == walletUpdateModel.WalletId);
            if (wallet == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Wallet Not Found",
                    Data = null
                };
            }

            wallet.Balance += walletUpdateModel.Amount;
            _walletRepository.Update(wallet);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<WalletResponseModel>
            {
                IsSuccessful = true,
                Message = "Wallet Updated",
                Data = new WalletResponseModel
                {
                    Id = wallet.Id,
                    FullName = $"{wallet.Patient.User.FirstName} {wallet.Patient.User.FirstName}",
                    Balance = wallet.Balance
                }
            };
        }
    }
}
