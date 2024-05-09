using HMSMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSMVC.Entity
{
    public class Service : Auditables
    {
        public required string ServiceName { get; set; }
        public string? ServiceDescription { get; set; }
        public required decimal Amount { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<PatientServices> PatientServices { get; set; } = new List<PatientServices>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
