using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentService(IServiceRepository serviceRepository, IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, IPatientRepository patientRepository, IHttpContextAccessor contextAccessor)
        {
            _serviceRepository = serviceRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<AppointmentResponseModel>> CreateAsync(AppointmentRequestModel requestModel)
        {
            var service = await _serviceRepository.GetAsync(s => s.Id == requestModel.ServiceId);
            if (service == null) return new BaseResponse<AppointmentResponseModel>
            {
                IsSuccessful = false,
                Message = "Service not Available",
                Data = null
            };

            var patient = await _patientRepository.GetAsync(p => p.Id == requestModel.PatientId);
            var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var appointment = new Appointment
            {
                PatientId = requestModel.PatientId,
                Patient = patient,
                ServiceId = requestModel.ServiceId,
                Service = service,
                Complain = requestModel.Complain,
                Status = AppointmentStatus.Pending,
                AppointmentDateAndTime = DateTime.Now,
                CreatedBy = loginUser,
                CreatedOn = DateTime.Now,
            };
            await _appointmentRepository.AddAsync(appointment);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<AppointmentResponseModel>
            {
                IsSuccessful = true,
                Message = "Appointment Schedulled Succesfully",
                Data = new AppointmentResponseModel
                {
                    Id = appointment.Id,
                    PatientFullName = $"{patient.User.FirstName} {patient.User.LastName}",
                    ServiceName = service.ServiceName,
                    Status = AppointmentStatus.Pending,
                    AppointmentDateAndTime = DateTime.Now,
                }
            };
        }

        public async Task<BaseResponse<AppointmentResponseModel>> DeletAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetAsync(a => a.Id == id);
            _appointmentRepository.Delete(appointment);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<AppointmentResponseModel>
            {
                IsSuccessful = true,
                Message = "Appointment Deleted Successfully",
                Data = null
            };
        }

        public async Task<BaseResponse<ICollection<AppointmentResponseModel>>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            var appointmentsView = appointments.Select(appointment => new AppointmentResponseModel
            {
                Id = appointment.Id,
                PatientFullName = $"{appointment.Patient.User.FirstName} {appointment.Patient.User.LastName}",
                ServiceName = appointment.Service.ServiceName,
                Status = appointment.Status,
                AppointmentDateAndTime = appointment.AppointmentDateAndTime
            }).ToList();

            return new BaseResponse<ICollection<AppointmentResponseModel>>
            {
                IsSuccessful = true,
                Message = "Succesfully Found",
                Data = appointmentsView
            };
        }

        public async Task<BaseResponse<AppointmentResponseModel>> GetAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetAsync(a => a.Id == id);
            if (appointment == null) return new BaseResponse<AppointmentResponseModel>
            {
                IsSuccessful = false,
                Message = "Appoinment not Found",
                Data = null
            };

            return new BaseResponse<AppointmentResponseModel>
            {
                IsSuccessful = true,
                Message = "Appointment Found",
                Data = new AppointmentResponseModel
                {   
                    Id = appointment.Id,
                    PatientFullName = $"{appointment.Patient.User.FirstName} {appointment.Patient.User.LastName}",
                    ServiceName = appointment.Service.ServiceName,
                    Status = appointment.Status,
                    AppointmentDateAndTime = appointment.AppointmentDateAndTime
                }
            };
        }

		public async Task<BaseResponse<AppointmentResponseModel>> UpdateAsync(Guid id, AppointmentUpdateModel appointmentUpdateModel)
		{
            var appointment = await _appointmentRepository.GetAsync(a => a.Id == id);
			if (appointment == null) return new BaseResponse<AppointmentResponseModel>
			{
				IsSuccessful = false,
				Message = "Appoinment not Found",
				Data = null
			};

			var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            appointment.DoctorId = appointmentUpdateModel.DoctorId;
            appointment.DepartmentId = appointmentUpdateModel.DepartmentId;
            appointment.Status = appointmentUpdateModel.Status;
            appointment.ModifiedBy = loginUser;
            appointment.ModifiedOn = DateTime.Now;

			return new BaseResponse<AppointmentResponseModel>
			{
				IsSuccessful = true,
				Message = "Appointment Updated",
				Data = new AppointmentResponseModel
				{
					Id = appointment.Id,
					PatientFullName = $"{appointment.Patient.User.FirstName} {appointment.Patient.User.LastName}",
					ServiceName = appointment.Service.ServiceName,
					Status = appointment.Status,
					AppointmentDateAndTime = appointment.AppointmentDateAndTime
				}
			};
		}
	}
}
