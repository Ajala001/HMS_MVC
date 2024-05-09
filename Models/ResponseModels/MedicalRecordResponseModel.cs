using HMSMVC.Entities;
using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.ResponseModels
{
	public class MedicalRecordResponseModel
	{
		public Guid Id { get; set; }
		public Guid AppointmentId { get; set; } = default!;
		public string PatientFullname { get; set; } = default!;
		public DateTime PatientDateOfBirth { get; set; }
		public Gender Gender { get; set; }
		public string Address { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string PatientCode { get; set; } = default!;
		public DateTime MedicalRecordDate { get; set; }
		public string Complains { get; set; } = default!;
		public string DoctorDiagnois { get; set; } = default!;
		public string SignatureUrl { get; set; } = default!;
		public Recommendation DoctorRecommendations { get; set; } = default!;
	}
}
