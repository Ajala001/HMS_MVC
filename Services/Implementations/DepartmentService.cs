using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }
        public async Task<BaseResponse<DepartmentResponseModel>> CreateAsync(DepartmentRequestModel requestModel)
        {
            var departmentExist = await _departmentRepository.IsExistAsync(requestModel.DepartmentName);
            if (departmentExist) return new BaseResponse<DepartmentResponseModel>
            {
                IsSuccessful = false,
                Message = "Deparment Already Exist",
                Data = null
            };

            var loginUser = _httpContext.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var department = new Department
            {
                DepartmentName = requestModel.DepartmentName,
                DepartmentDescription = requestModel.DepartmentDescription,
                OpeningHours = requestModel.OpeningHours,
                ClosingHours = requestModel.ClosingHours,
                CreatedBy = loginUser,
                CreatedOn = DateTime.Now
            };
            await _departmentRepository.AddAsync(department);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<DepartmentResponseModel>
            {
                IsSuccessful = true,
                Message = "Department Created Succesfull",
                Data = new DepartmentResponseModel
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    OpeningHours = department.OpeningHours,
                    ClosingHours = department.ClosingHours
                }
            };
        }

        public async Task<BaseResponse<DepartmentResponseModel>> DeleteAsync(Guid id)
        {
            var department = await _departmentRepository.GetAsync(d => d.Id == id);
            _departmentRepository.Delete(department);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<DepartmentResponseModel>
            {
                IsSuccessful = true,
                Message = "Department Deleted Succesfull",
                Data = null
            };
        }

        public async Task<BaseResponse<ICollection<DepartmentResponseModel>>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentsView = departments.Select(department => new DepartmentResponseModel
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                OpeningHours = department.OpeningHours,
                ClosingHours = department.ClosingHours
            }).ToList();

            return new BaseResponse<ICollection<DepartmentResponseModel>>
            {
                IsSuccessful = true,
                Message = "Successfully Found",
                Data = departmentsView
            };
        }

        public async Task<BaseResponse<DepartmentResponseModel>> GetAsync(Guid id)
        {
            var department = await _departmentRepository.GetAsync(d => d.Id == id);
            if (department == null) return new BaseResponse<DepartmentResponseModel>
            {
                IsSuccessful = false,
                Message = "Not Found",
                Data = null
            };

            return new BaseResponse<DepartmentResponseModel>
            {
                IsSuccessful = true,
                Message = "Department Found",
                Data = new DepartmentResponseModel
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    OpeningHours = department.OpeningHours,
                    ClosingHours = department.ClosingHours
                }
            };
        }

		public async Task<BaseResponse<DepartmentResponseModel>> UpdateAsync(Guid id, DepartmentUpdateModel departmentUpdateModel)
		{
            var department = await _departmentRepository.GetAsync(d => d.Id == id);
            if(department == null)
            {
                return new BaseResponse<DepartmentResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Department Not Found",
                    Data = null
                };
            }

			var loginUser = _httpContext.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
			department.DepartmentName = departmentUpdateModel.DepartmentName ?? department.DepartmentName;
            department.DepartmentDescription = departmentUpdateModel.DepartmentDescription ?? department.DepartmentDescription;
            department.HeadOfDepartment = departmentUpdateModel.HeadOfDepartment ?? department.HeadOfDepartment;
            department.OpeningHours = departmentUpdateModel.OpeningHours;
            department.ClosingHours = departmentUpdateModel.ClosingHours;
            department.ModifiedBy = loginUser;
            department.ModifiedOn = DateTime.Now;

            return new BaseResponse<DepartmentResponseModel>
            {

				IsSuccessful = true,
				Message = "Department Updated",
				Data = new DepartmentResponseModel
				{
					Id = department.Id,
					DepartmentName = department.DepartmentName,
					OpeningHours = department.OpeningHours,
					ClosingHours = department.ClosingHours
				}
			};
		}
	}
}
