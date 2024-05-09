using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Data;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUserRepository userRepository, IRoleRepository roleRepository, IPatientRepository patientRepository, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }
        public async Task<BaseResponse<PatientResponseModel>> CreateAsync(PatientRequestModel patientRequestModel)
        {
            var user = await _userRepository.GetAsync(u => u.Email == patientRequestModel.UserEmail);
            if(user == null)
            {
                return new BaseResponse<PatientResponseModel>
                {
                    IsSuccessful = false,
                    Message = "You Are not A Registered User",
                    Data = null
                };
            }

            var roleExist = user.UserRoles.Any(r => r.Role.Name == "Patient");
            if(roleExist)
            {
                return new BaseResponse<PatientResponseModel>
                {
                    IsSuccessful = false,
                    Message = "You Are Already A Registered Patient",
                    Data = null
                };
            }
            
            var newRole = await _roleRepository.GetAsync(r => r.Name == "Patient");
            if(newRole == null)
            {
                return new BaseResponse<PatientResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Patient Role does not exist",
                    Data = null
                };
            }

            var userRoles = new UserRoles
            {
                User = user,
                UserId = user.Id,
                Role = newRole,
                RoleId = newRole.Id
            };
            user.UserRoles.Add(userRoles);
            _userRepository.Update(user);

            var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                PatientCode = $"HMS/{Guid.NewGuid().ToString().Substring(1, 3).ToUpper()}",
                UserId = user.Id,
                User = user,
                HasScheduleAppointment = false,
                CreatedBy = loginUser,
                CreatedOn = DateTime.Now
            };
            await _patientRepository.AddAsync(patient);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<PatientResponseModel>
            {
                IsSuccessful = true,
                Message = "Patient Created Successfull",
                Data = new PatientResponseModel
                {
                    Id = patient.Id,
                    FullName = $"{user.LastName} {user.FirstName}",
                    PatientCode = patient.PatientCode,
                    Email = patient.User.Email,
                    Gender = patient.User.Gender,
                    PhoneNumber = patient.User.PhoneNumber
                }
            };
        }

        public async Task<BaseResponse<PatientResponseModel>> DeleteAsync(Guid id)
        {
            var patient = await _patientRepository.GetAsync(p =>  p.Id == id);
            var user = await _userRepository.GetAsync(u => u.Id == patient.UserId);

            _userRepository.Delete(user);
            _patientRepository.Delete(patient);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<PatientResponseModel>
            {
                IsSuccessful = true,
                Message = "Delete Successfull",
                Data = null
            };
        }

        public async Task<BaseResponse<ICollection<PatientResponseModel>>> GetAllAsync()
        {
            var patients = await _patientRepository.GetAllAsync();

            var patientsView = patients.Select(patient => new PatientResponseModel
            {
                Id = patient.Id,
                FullName = $"{patient.User.LastName} {patient.User.FirstName}",
                PatientCode = patient.PatientCode,
                Email = patient.User.Email,
                Gender = patient.User.Gender,
                PhoneNumber = patient.User.PhoneNumber
            }).ToList();

            return new BaseResponse<ICollection<PatientResponseModel>>
            {
                IsSuccessful = true,
                Message = "Successfully Found",
                Data = patientsView
            };
        }

        public async Task<BaseResponse<PatientResponseModel>> GetAsync(Guid id)
        {
            var patient = await _patientRepository.GetAsync(p => p.Id == id);

            if(patient  == null)
            {
                return new BaseResponse<PatientResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Patient Not Found",
                    Data = null
                };
            }


            return new BaseResponse<PatientResponseModel>
            {
                IsSuccessful = true,
                Message = "Patient Found",
                Data = new PatientResponseModel
                {
                    Id = patient.Id,
                    FullName = $"{patient.User.LastName} {patient.User.FirstName}",
                    PatientCode = patient.PatientCode,
                    Email = patient.User.Email,
                    Gender = patient.User.Gender,
                    PhoneNumber = patient.User.PhoneNumber
                }
            };
        }

        public async Task<BaseResponse<PatientResponseModel>> UpdateAsync(Guid id, PatientUpdateModel patientUpdateModel)
        {
            var patient = await _patientRepository.GetAsync(p => p.Id == id);
            if(patient == null)
            {
                return new BaseResponse<PatientResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Patient Not Found",
                    Data = null
                };
            }

			var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
			patient.User.FirstName = patientUpdateModel.FirstName ?? patient.User.FirstName;
            patient.User.LastName = patientUpdateModel.LastName ?? patient.User.LastName;
            patient.User.Email = patientUpdateModel.Email ?? patient.User.Email;
            patient.User.PhoneNumber = patientUpdateModel.PhoneNumber ?? patient.User.PhoneNumber;
            patient.User.Gender = patientUpdateModel.Gender ?? patient.User.Gender;
            patient.User.DateOfBirth = patientUpdateModel.DateOfBirth ?? patient.User.DateOfBirth;
            patient.User.ImageUrl = patientUpdateModel.ImageUrl ?? patient.User.ImageUrl;
            patient.User.ModifiedOn = DateTime.Now;
            patient.User.ModifiedBy = loginUser;
            

            _patientRepository.Update(patient);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<PatientResponseModel>
            {
                IsSuccessful = true,
                Message = "Updated Successfully",
                Data = new PatientResponseModel
                {
                    Id = patient.Id,
                    FullName = $"{patient.User.LastName} {patient.User.FirstName}",
                    PatientCode = patient.PatientCode,
                    Email = patient.User.Email,
                    Gender = patient.User.Gender,
                    PhoneNumber = patient.User.PhoneNumber
                }
            };
        }
    }
}
