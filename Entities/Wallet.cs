
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSMVC.Entity
{
    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey ("PatientId")]
        public Guid? PatientId { get; set; }
        public Patient? Patient { get; set; }
        public decimal Balance { get; set; }
    }
}
