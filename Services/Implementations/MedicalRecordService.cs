using HMSMVC.Entities;
using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
	public class MedicalRecordService : IMedicalRecordService
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IMedicalRecordRepository _medicalRecordRepository;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IUnitOfWork _unitOfWork;

		public MedicalRecordService(IAppointmentRepository appointmentRepository, IMedicalRecordRepository medicalRecordRepository, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
		{
			_appointmentRepository = appointmentRepository;
			_medicalRecordRepository = medicalRecordRepository;
			_contextAccessor = contextAccessor;
			_unitOfWork = unitOfWork;
		}
		public async Task<BaseResponse<MedicalRecordResponseModel>> CreateAsync(MedicalRecordRequestModel medicalRecordRequestModel)
		{
			var appointment = await _appointmentRepository.GetAsync(a => a.Id == medicalRecordRequestModel.AppointmentId);
			if (appointment == null) return new BaseResponse<MedicalRecordResponseModel>
			{
				IsSuccessful = false,
				Message = "Appointment Not Found",
				Data = null
			};

			if (appointment.Status != AppointmentStatus.Completed) return new BaseResponse<MedicalRecordResponseModel>
			{
				IsSuccessful = false,
				Message = "Appointment Not Completed",
				Data = null
			};

			var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
			var medicalRecord = new MedicalRecord
			{
				Id = Guid.NewGuid(),
				AppointmentId = appointment.Id,
				Appointment = appointment,
				DateRecorded = medicalRecordRequestModel.DateRecorded,
				DoctorReport = medicalRecordRequestModel.DoctorReport,
				MedicalRecDept = appointment.Doctor.Department.DepartmentName,
				CreatedBy = loginUser,
				CreatedOn = DateTime.Now
			};

			var doctorRecommendations = new Recommendation
			{
				Id = Guid.NewGuid(),
				DoctorRecommendation1 = medicalRecordRequestModel.DoctorRecommendation1,
				DoctorRecommendation2 = medicalRecordRequestModel.DoctorRecommendation2,
				DoctorRecommendation3 = medicalRecordRequestModel.DoctorRecommendation3,
				DoctorRecommendation4 = medicalRecordRequestModel.DoctorRecommendation4,
				DoctorRecommendation5 = medicalRecordRequestModel.DoctorRecommendation5,
				MedicalRecordId = medicalRecord.Id,
				MedicalRecord = medicalRecord
			};
			medicalRecord.DoctorRecommendations = doctorRecommendations;

			await _medicalRecordRepository.AddAsync(medicalRecord);
			await _unitOfWork.SaveAsync();

			return new BaseResponse<MedicalRecordResponseModel>
			{
				IsSuccessful = true,
				Message = "Medical Record Created Succesfully",
				Data = new MedicalRecordResponseModel
				{
					Id = medicalRecord.Id,
					AppointmentId = medicalRecord.AppointmentId,
					PatientFullname = $"{medicalRecord.Appointment.Patient.User.FirstName} {medicalRecord.Appointment.Patient.User.LastName}",
					PatientDateOfBirth = medicalRecord.Appointment.Patient.User.DateOfBirth,
					Gender = medicalRecord.Appointment.Patient.User.Gender,
					Address = medicalRecord.Appointment.Patient.User.Address,
					PhoneNumber = medicalRecord.Appointment.Patient.User.PhoneNumber,
					Email = medicalRecord.Appointment.Patient.User.Email,
					PatientCode = medicalRecord.Appointment.Patient.PatientCode,
					MedicalRecordDate = medicalRecord.DateRecorded,
					Complains = medicalRecord.Appointment.Complain,
					DoctorDiagnois = medicalRecord.DoctorReport,
					SignatureUrl = medicalRecord.Appointment.Doctor.SignatureUrl,
					DoctorRecommendations = medicalRecord.DoctorRecommendations
				}
			};
		}

		public async Task<BaseResponse<MedicalRecordResponseModel>> DeleteAsync(Guid medicalRecordId)
		{
			var medicalRecord = await _medicalRecordRepository.GetAsync(m => m.Id == medicalRecordId);
			if (medicalRecord == null) return new BaseResponse<MedicalRecordResponseModel>
			{
				IsSuccessful = false,
				Message = "Not Found",
				Data = null
			};

			_medicalRecordRepository.Delete(medicalRecord);
			return new BaseResponse<MedicalRecordResponseModel>
			{

				IsSuccessful = true,
				Message = "Deleted Succesfully",
				Data = null
			};
		}

		public async Task<BaseResponse<ICollection<MedicalRecordResponseModel>>> GetAllAsync()
		{
			var medicalRecords = await _medicalRecordRepository.GetAllAsync();
			if (medicalRecords.Count == 0) return new BaseResponse<ICollection<MedicalRecordResponseModel>>
			{
				IsSuccessful = false,
				Message = "No Medical Records",
				Data = null
			};

			var medicalRecordsView = medicalRecords.Select(medicalRecord => new MedicalRecordResponseModel
			{
				Id = medicalRecord.Id,
				AppointmentId = medicalRecord.AppointmentId,
				PatientFullname = $"{medicalRecord.Appointment.Patient.User.FirstName} {medicalRecord.Appointment.Patient.User.LastName}",
				PatientDateOfBirth = medicalRecord.Appointment.Patient.User.DateOfBirth,
				Gender = medicalRecord.Appointment.Patient.User.Gender,
				Address = medicalRecord.Appointment.Patient.User.Address,
				PhoneNumber = medicalRecord.Appointment.Patient.User.PhoneNumber,
				Email = medicalRecord.Appointment.Patient.User.Email,
				PatientCode = medicalRecord.Appointment.Patient.PatientCode,
				MedicalRecordDate = medicalRecord.DateRecorded,
				Complains = medicalRecord.Appointment.Complain,
				DoctorDiagnois = medicalRecord.DoctorReport,
				SignatureUrl = medicalRecord.Appointment.Doctor.SignatureUrl,
				DoctorRecommendations = medicalRecord.DoctorRecommendations
			}).ToList();

			return new BaseResponse<ICollection<MedicalRecordResponseModel>>()
			{
				IsSuccessful = true,
				Message = "Medical Records Found",
				Data = medicalRecordsView
			};
		}

		public async Task<BaseResponse<ICollection<MedicalRecordResponseModel>>> GetMedicalRecordAsync(Guid patientId)
		{
			var medicalRecords = await _medicalRecordRepository.GetSelectedAsync(m => m.Appointment.PatientId == patientId);
			if (medicalRecords.Count == 0) return new BaseResponse<ICollection<MedicalRecordResponseModel>>
			{
				IsSuccessful = false,
				Message = "No Medical Records",
				Data = null
			};

			var medicalRecordsView = medicalRecords.Select(medicalRecord => new MedicalRecordResponseModel
			{
				Id = medicalRecord.Id,
				AppointmentId = medicalRecord.AppointmentId,
				PatientFullname = $"{medicalRecord.Appointment.Patient.User.FirstName} {medicalRecord.Appointment.Patient.User.LastName}",
				PatientDateOfBirth = medicalRecord.Appointment.Patient.User.DateOfBirth,
				Gender = medicalRecord.Appointment.Patient.User.Gender,
				Address = medicalRecord.Appointment.Patient.User.Address,
				PhoneNumber = medicalRecord.Appointment.Patient.User.PhoneNumber,
				Email = medicalRecord.Appointment.Patient.User.Email,
				PatientCode = medicalRecord.Appointment.Patient.PatientCode,
				MedicalRecordDate = medicalRecord.DateRecorded,
				Complains = medicalRecord.Appointment.Complain,
				DoctorDiagnois = medicalRecord.DoctorReport,
				SignatureUrl = medicalRecord.Appointment.Doctor.SignatureUrl,
				DoctorRecommendations = medicalRecord.DoctorRecommendations
			}).ToList();

			return new BaseResponse<ICollection<MedicalRecordResponseModel>>()
			{
				IsSuccessful = true,
				Message = "Medical Records Found",
				Data = medicalRecordsView
			};
		}
	}
}
