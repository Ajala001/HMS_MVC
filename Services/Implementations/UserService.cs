using BCrypt.Net;
using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using HMSMVC.Services.ValidationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(RegistrationModel registrationModel)
        {
            var user = new User()
            {
                UserName = registrationModel.Email,
                Email = registrationModel.Email,
                Password = registrationModel.Password,
                PhoneNumber = registrationModel.PhoneNumber,
                Address = registrationModel.Address,
                CreatedBy = "system",
                CreatedOn = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, registrationModel.Password);
            return result;
        }
        public async Task<BaseResponse<UserResponseModel>> CreateAsync(RegistrationModel userRequestModel)
        {
            var validEmail = Validations.ValidateEmail(userRequestModel.Email);
            if (!validEmail)
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = $"Email {userRequestModel.Email} doesn't match requirement",
                    Data = null
                };
            }

            var validPassword = Validations.ValidatePassword(userRequestModel.Password);
            if (!validPassword)
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = $"Password doesn't match requirement",
                    Data = null
                };
            }

            var validPhoneNumber = Validations.ValidatePhoneNumber(userRequestModel.PhoneNumber);
            if (!validPhoneNumber)
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = $"Phone Number doesn't match requirement",
                    Data = null
                };
            }

            //var validPin = Validations.ValidatePin(userRequestModel.Pin);
            //if (!validPin)
            //{
            //    return new BaseResponse<UserResponseModel>
            //    {
            //        IsSuccessful = false,
            //        Message = $"Pin doesn't match requirement",
            //        Data = null
            //    };
            //}

            var userExist = await _userRepository.IsExistAsync(userRequestModel.Email);
            if (userExist)
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Email Already Exist",
                    Data = null
                };
            }

            if (userRequestModel.Password != userRequestModel.ConfirmPassword)
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Password Missmatch",
                    Data = null
                };
            }

            var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userRequestModel.Password);
            var user = new User
            {
                Email = userRequestModel.Email,
                Password = hashPassword,
                PhoneNumber = userRequestModel.PhoneNumber,
                CreatedBy = loginUser,
                CreatedOn = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<UserResponseModel>
            {
                IsSuccessful = true,
                Message = "You Have Succesfully Registered",
                Data = new UserResponseModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    ImageUrl = user.ImageUrl,
                    Roles = user.UserRoles.Select(r => new RoleResponseModel
                    {
                        Name = r.Role.Name,
                    }).ToList()
                }
            };
        }

        public async Task<SignInResult> PasswordSignInAsync(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
            return result;
        }

        public async Task<BaseResponse<ICollection<UserResponseModel>>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users.Count == 0)
            {
                return new BaseResponse<ICollection<UserResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "No User Has Registered",
                    Data = null
                };
            }

            var usersView = users.Select(user => new UserResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                ImageUrl = user.ImageUrl,
                Roles = user.UserRoles.Select(r => new RoleResponseModel
                {
                    Name = r.Role.Name,
                }).ToList()
            }).ToList();

            return new BaseResponse<ICollection<UserResponseModel>>()
            {
                IsSuccessful = true,
                Message = "Users Found",
                Data = usersView
            };
        }

        public async Task<BaseResponse<UserResponseModel>> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(u => u.Id == id);
            if(user == null)
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = "User Not Found",
                    Data = null
                };
            }

            return new BaseResponse<UserResponseModel>
            {
                IsSuccessful = true,
                Message = "User Found",
                Data = new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    ImageUrl = user.ImageUrl,
                    Roles = user.UserRoles.Select(r => new RoleResponseModel
                    {
                        Name = r.Role.Name,
                    }).ToList()
                }
            };
        }

        public async Task<BaseResponse<UserResponseModel>> LoginAsync(LoginModel loginRequestModel)
        {
            var user = await _userRepository.GetAsync(u => u.Password == loginRequestModel.Password);
            //if(user == null)
            //{
            //    return new BaseResponse<UserResponseModel>
            //    {
            //        IsSuccessful = false,
            //        Message = "User Not Recognized",
            //        Data = null
            //    };
            //}

            if (!BCrypt.Net.BCrypt.Verify(loginRequestModel.Password, user.Password))
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Incorrect Password",
                    Data = null
                };
            }

            //if(user.Pin != loginRequestModel.Pin)
            //{
            //    return new BaseResponse<UserResponseModel>
            //    {
            //        IsSuccessful = false,
            //        Message = "Incorrect Pin",
            //        Data = null
            //    };
            //}

            return new BaseResponse<UserResponseModel>
            {
                IsSuccessful = true,
                Message = "User Found",
                Data = new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    ImageUrl = user.ImageUrl,
                    Roles = user.UserRoles.Select(r => new RoleResponseModel
                    {
                        Name = r.Role.Name,
                    }).ToList()
                }
            };

        }

        public async Task<BaseResponse<UserResponseModel>> UpdateAsync(Guid id, UserUpdateModel userUpdateModel)
        {
            var user = await _userRepository.GetAsync(u => u.Id == id);
            if(user == null)
            {
                return new BaseResponse<UserResponseModel>
                {
                    IsSuccessful = false,
                    Message = "User Not Found",
                    Data = null
                };
            }

			var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
			user.FirstName = userUpdateModel.FirstName ?? user.FirstName;
            user.LastName = userUpdateModel.LastName ?? user.LastName;
            user.PhoneNumber = userUpdateModel.PhoneNumber ?? user.PhoneNumber;
            user.DateOfBirth = userUpdateModel.DateOfBirth ?? user.DateOfBirth;
            user.ImageUrl = userUpdateModel.ImageUrl ?? user.ImageUrl;
            user.Email = userUpdateModel.Email ?? user.Email;
            user.Gender = userUpdateModel.Gender ?? user.Gender;
            user.Address = userUpdateModel.Address ?? user.Address;
            user.ModifiedBy = loginUser;
            user.ModifiedOn = DateTime.Now;

            _userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<UserResponseModel>
            {
                IsSuccessful = true,
                Message = "User Updated",
                Data = new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    ImageUrl = user.ImageUrl,
                    Roles = user.UserRoles.Select(r => new RoleResponseModel
                    {
                        Name = r.Role.Name,
                    }).ToList()
                }
            };
        }
    }
}
