using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.RequestModels
{
	public class MedicalRecordRequestModel
	{
		public Guid AppointmentId { get; set; } = default!;
		public DateTime DateRecorded { get; set; }
		public string? DoctorReport { get; set; }
		public required string DoctorRecommendation1 { get; set; }
		public required string DoctorRecommendation2 { get; set; }
		public required string DoctorRecommendation3 { get; set; }
		public string? DoctorRecommendation4 { get; set; }
		public string? DoctorRecommendation5 { get; set; }
	}
}
