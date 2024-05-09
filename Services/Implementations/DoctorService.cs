using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public DoctorService(IUserRepository userRepository, IRoleRepository roleRepository, IDepartmentRepository departmentRepository, IDoctorRepository doctorRepository, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _departmentRepository = departmentRepository;
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }
        public async Task<BaseResponse<DoctorResponseModel>> CreateAsync(DoctorRequestModel doctorRequestModel)
        {
            var user = await _userRepository.GetAsync(u => u.Email == doctorRequestModel.UserEmail);
            if (user == null)
            {
                return new BaseResponse<DoctorResponseModel>
                {
                    IsSuccessful = false,
                    Message = "You Are Not A Registered User",
                    Data = null
                };
            }

            var roleExist = user.UserRoles.Any(r => r.Role.Name == "Doctor");
            if (roleExist)
            {
                return new BaseResponse<DoctorResponseModel>
                {
                    IsSuccessful = false,
                    Message = "You Are Already A Registered Doctor",
                    Data = null
                };
            }

            var department = await _departmentRepository.GetAsync(d => d.Id == doctorRequestModel.DepartmentId);
            if(department == null)
            {
                return new BaseResponse<DoctorResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Department Entered Do Not Exist",
                    Data = null
                };
            }

            var newRole = await _roleRepository.GetAsync(r => r.Name == "Doctor");
            if (newRole == null)
            {
                return new BaseResponse<DoctorResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Doctor Role does not exist",
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
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                DoctorCode = $"HMS/{Guid.NewGuid().ToString().Substring(1, 3).ToUpper()}",
                UserId = user.Id,
                User = user,
                Department = department,
                DepartmentId = department.Id,
                YearOfExperience = doctorRequestModel.YearOfExperience,
                FieldOfSpecialization = doctorRequestModel.FieldOfSpecialization,
                CreatedBy = loginUser,
                CreatedOn = DateTime.Now
            };
            await _doctorRepository.AddAsync(doctor);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<DoctorResponseModel>
            {
                IsSuccessful = true,
                Message = "Doctor Created Succesfully",
                Data = new DoctorResponseModel
                {
                    Id = doctor.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DepartmentName = department.DepartmentName,
                    Gender = user.Gender
                }
            };
        }

        public async Task<BaseResponse<DoctorResponseModel>> DeleteAsync(Guid id)
        {
            var doctor = await _doctorRepository.GetAsync(d => d.Id == id);
            var user = await _userRepository.GetAsync(u => u.Id == doctor.UserId);

            _userRepository.Delete(user);
            _doctorRepository.Delete(doctor);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<DoctorResponseModel>
            {
                IsSuccessful = true,
                Message = "Doctor Deleted Succesfully",
                Data = null
            };
        }

        public async Task<BaseResponse<ICollection<DoctorResponseModel>>> GetAllAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            var doctorsView = doctors.Select(doctor => new DoctorResponseModel
            {
                Id = doctor.Id,
                FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
                Email = doctor.User.Email,
                PhoneNumber = doctor.User.PhoneNumber,
                DepartmentName = doctor.Department.DepartmentName,
                Gender = doctor.User.Gender
            }).ToList();

            return new BaseResponse<ICollection<DoctorResponseModel>>
            {
                IsSuccessful = true,
                Message = "Successfully Found",
                Data = doctorsView
            };
        }

        public async Task<BaseResponse<DoctorResponseModel>> GetAsync(Guid id)
        {
            var doctor = await _doctorRepository.GetAsync(d => d.Id == id);
            if(doctor == null)
            {
                return new BaseResponse<DoctorResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Doctor not Found",
                    Data = null
                };
            }

            return new BaseResponse<DoctorResponseModel>
            {
                IsSuccessful = true,
                Message = "Doctor Found",
                Data = new DoctorResponseModel
                {
                    Id = doctor.Id,
                    FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
                    Email = doctor.User.Email,
                    PhoneNumber = doctor.User.PhoneNumber,
                    DepartmentName = doctor.Department.DepartmentName,
                    Gender = doctor.User.Gender
                }
            };
        }

        public async Task<BaseResponse<ICollection<DoctorResponseModel>>> GetSelectedAsync(string departmentName)
        {
            var doctors = await _doctorRepository.GetSelectedAsync(d => d.Department.DepartmentName == departmentName);
            if(!doctors.Any())
            {
                return new BaseResponse<ICollection<DoctorResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "Department Has No Doctor",
                    Data = null
                };
            }

            var doctorsView = doctors.Select(doctor => new DoctorResponseModel
            {
                Id = doctor.Id,
                FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
                Email = doctor.User.Email,
                PhoneNumber = doctor.User.PhoneNumber,
                DepartmentName = doctor.Department.DepartmentName,
                Gender = doctor.User.Gender
            }).ToList();

            return new BaseResponse<ICollection<DoctorResponseModel>>
            {
                IsSuccessful = true,
                Message = "Successfully Found",
                Data = doctorsView
            };
        }

        public async Task<BaseResponse<DoctorResponseModel>> UpdateAsync(Guid id, DoctorUpdateModel updateModel)
        {
            var doctor = await _doctorRepository.GetAsync(d => d.Id == id);
            if(doctor == null)
            {
                return new BaseResponse<DoctorResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Doctor not Found",
                    Data = null
                };
            }

            var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            doctor.User.FirstName = updateModel.FirstName ?? doctor.User.FirstName;
            doctor.User.LastName = updateModel.LastName ?? doctor.User.LastName;
            doctor.User.PhoneNumber = updateModel.PhoneNumber ?? doctor.User.PhoneNumber;
            doctor.User.Email = updateModel.Email ?? doctor.User.Email;
            doctor.User.Gender = updateModel.Gender ?? doctor.User.Gender;
            doctor.User.DateOfBirth = updateModel.DateOfBirth ?? doctor.User.DateOfBirth;
            doctor.User.ImageUrl = updateModel.ImageUrl ?? doctor.User.ImageUrl;
            doctor.YearOfExperience = updateModel.YearOfExperience ?? doctor.YearOfExperience;
            doctor.FieldOfSpecialization = updateModel.FieldOfSpecialization ?? doctor.FieldOfSpecialization;
            doctor.SignatureUrl = updateModel.SignatureUrl ?? doctor.SignatureUrl;
            doctor.User.ModifiedBy = loginUser;
            doctor.User.ModifiedOn = DateTime.Now;

            
            _doctorRepository.Update(doctor);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<DoctorResponseModel>
            {
                IsSuccessful = true,
                Message = "Doctor Successfully Updated",
                Data = new DoctorResponseModel
                {
                    Id = doctor.Id,
                    FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
                    Email = doctor.User.Email,
                    PhoneNumber = doctor.User.PhoneNumber,
                    DepartmentName = doctor.Department.DepartmentName,
                    Gender = doctor.User.Gender
                }
            };
        }
    }
}
