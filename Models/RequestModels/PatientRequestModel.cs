using HMSMVC.Entities;
using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.RequestModels
{
    public class PatientRequestModel
    {
        public required string UserEmail { get; set; } 
        public required string Pin { get; set; }
        public required string Password { get; set; }
        [Compare("ConfirmPassword")]
        public required string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
