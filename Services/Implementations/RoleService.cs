using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IHttpContextAccessor contextAccessor)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }
        public async Task<BaseResponse<RoleResponseModel>> CreateAsync(RoleRequestModel roleRequeatModel)
        {
            var role = await _roleRepository.GetAsync(r => r.Name == roleRequeatModel.Name);
            if(role != null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Role Already Exist",
                    Data = null
                };
            }

            var newRole = new Role
            {
                Name = roleRequeatModel.Name!,
                Description = roleRequeatModel.Description,
            };
            await _roleRepository.AddAsync(newRole);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<RoleResponseModel>
            {
                IsSuccessful = true,
                Message = "Role Created Successfully",
                Data = new RoleResponseModel
                {
                    Id = newRole.Id,
                    Name = newRole.Name,
                    Description = newRole.Description,
                }
            };
        }

        public async Task<BaseResponse<RoleResponseModel>> DeleteAsync(Guid roleId)
        {
            var role = await _roleRepository.GetAsync(r => r.Id == roleId);
            if(role == null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Role Not Found",
                    Data = null
                };
            }

            _roleRepository.Delete(role);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<RoleResponseModel>
            {
                IsSuccessful = true,
                Message = "Role Deleted",
                Data = null
            };
        }

        public async Task<BaseResponse<ICollection<RoleResponseModel>>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            if(roles.Count == 0)
            {
                return new BaseResponse<ICollection<RoleResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "Not Found",
                    Data = null
                };
            }

            var roleView = roles.Select(role => new RoleResponseModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            }).ToList();

            return new BaseResponse<ICollection<RoleResponseModel>>
            {
                IsSuccessful = true,
                Message = "Roles Found",
                Data = roleView
            };
        }

        public async Task<BaseResponse<RoleResponseModel>> GetAsync(Guid roleId)
        {
            var role = await _roleRepository.GetAsync(r => r.Id == roleId);
            if (role == null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Role Not Found",
                    Data = null
                };
            }

            return new BaseResponse<RoleResponseModel>
            {
                IsSuccessful = true,
                Message = "Role Found",
                Data = new RoleResponseModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                }
            };

        }

        public async Task<BaseResponse<RoleResponseModel>> UpdateAsync(RoleUpdateModel roleUpdateModel)
        {
            var role = await _roleRepository.GetAsync(r => r.Name == roleUpdateModel.Name);
            if(role == null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Not Found",
                    Data = null
                };
            }

			var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
			role.Name = roleUpdateModel.Name ?? role.Name;
            role.Description = roleUpdateModel.Description ?? role.Description;
            role.ModifiedBy = loginUser;
            role.ModifiedOn = DateTime.Now;
            _roleRepository.Update(role);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<RoleResponseModel>
            {
                IsSuccessful = true,
                Message = "Role Updated Successfully",
                Data = new RoleResponseModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                }
            };
        }
    }
}
