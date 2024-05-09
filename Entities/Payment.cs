using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSMVC.Entity
{
    public class Payment : Auditables
    {
        public Guid RefNumber { get; set; } = default!;
        [ForeignKey("PatientId")]
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
        public Service Service { get; set; } = default!;
        [ForeignKey("ServiceId")]
        public Guid ServiceId { get; set; }
        public decimal Amount { get; set; }

    }
}
