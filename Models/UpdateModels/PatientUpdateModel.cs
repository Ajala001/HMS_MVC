﻿namespace HMSMVC.Models.UpdateModels
{
    public class PatientUpdateModel
    {
        public string? LastName { get; set; } 
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
    }
}
