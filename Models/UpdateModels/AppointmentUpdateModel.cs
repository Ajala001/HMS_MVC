using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.UpdateModels
{
    public class AppointmentUpdateModel
    {
        public Guid? DoctorId { get; set; }
        public AppointmentStatus Status { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
