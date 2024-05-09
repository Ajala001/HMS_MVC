using HMSMVC.Entity;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IServiceRepository _serviceRepository;  
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IServiceRepository serviceRepository, IHttpContextAccessor ContextAccessor, IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, IPatientRepository patientRepository)
        {
            _serviceRepository = serviceRepository;
            _contextAccessor = ContextAccessor;
            _paymentRepository = paymentRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<PaymentResponseModel>> CreateAsync(Guid ServiceId)
        {
            var service = await _serviceRepository.GetAsync(s => s.Id == ServiceId);
            if(service  == null)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Service Not Available",
                    Data = null
                };
            }

            var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var patient = _patientRepository.GetAsync(p => p.Id == Guid.Parse(loginUser));
            
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                RefNumber = Guid.NewGuid(),
                PatientId = Guid.Parse(loginUser),
                ServiceId = service.Id,
                Service = service,
                Amount = service.Amount,
                CreatedBy = loginUser,
                CreatedOn = DateTime.Now
            };

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<PaymentResponseModel>
            {
                IsSuccessful = true,
                Message = "Payment Successfull",
                Data = new PaymentResponseModel
                {
                    Id = payment.Id,
                    FullName = $"{patient.Result.User.FirstName} {patient.Result.User.LastName}",
                    PaidFor = service.ServiceName,
                    RefNumber = payment.RefNumber,
                    PatientCode = patient.Result.PatientCode,
                    PaidOn = payment.CreatedOn
                }
            };
        }

        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            if(payments == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "No Payment Made",
                    Data = null
                };
            }

            var paymentsView = payments.Select(payment => new PaymentResponseModel
            {
                Id = payment.Id,
                FullName = $"{payment.Patient.User.FirstName} {payment.Patient.User.FirstName}",
                PaidFor = payment.Service.ServiceName,
                RefNumber = payment.RefNumber,
                PatientCode = payment.Patient.PatientCode,
                PaidOn = payment.CreatedOn
            }).ToList();

            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                IsSuccessful = true,
                Message = "Payments Found",
                Data = paymentsView
            };
        }

        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> GetPatientPaymentsAsync(Guid patientId)
        {
            var payments = await _paymentRepository.GetSelectedAsync(p => p.PatientId == patientId);
            if (payments == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "Patient Has Made Zero Payment",
                    Data = null
                };
            }

            var paymentsView = payments.Select(payment => new PaymentResponseModel
            {
                Id = payment.Id,
                FullName = $"{payment.Patient.User.FirstName} {payment.Patient.User.FirstName}",
                PaidFor = payment.Service.ServiceName,
                RefNumber = payment.RefNumber,
                PatientCode = payment.Patient.PatientCode,
                PaidOn = payment.CreatedOn
            }).ToList();

            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                IsSuccessful = true,
                Message = "Payments Found",
                Data = paymentsView
            };
        }

        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> GetServicePaymentsAsync(Guid serviceId)
        {
            var payments = await _paymentRepository.GetSelectedAsync(p => p.Service.Id == serviceId);
            if (payments == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "Service Has No Payment",
                    Data = null
                };
            }

            var paymentsView = payments.Select(payment => new PaymentResponseModel
            {

                Id = payment.Id,
                FullName = $"{payment.Patient.User.FirstName} {payment.Patient.User.FirstName}",
                PaidFor = payment.Service.ServiceName,
                RefNumber = payment.RefNumber,
                PatientCode = payment.Patient.PatientCode,
                PaidOn = payment.CreatedOn
            }).ToList();

            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                IsSuccessful = true,
                Message = "Payments Found",
                Data = paymentsView
            };
        }
    }
}
