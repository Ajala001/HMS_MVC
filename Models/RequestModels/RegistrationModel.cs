using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations;

namespace HMSMVC.Models.RequestModels
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Please Enter Your Email")]
        [EmailAddress(ErrorMessage = "Enter A Valid Email")]
        public required string Email { get; set; }

		[Required(ErrorMessage = "Please Enter A Strong Password")]
		[Compare("ConfirmPassword", ErrorMessage = "Password Does not Match")]
        [DataType(DataType.Password)]
		public required string Password { get; set; }

		[Required(ErrorMessage = "Please Confirm Your Password")]
		[DataType(DataType.Password)]
		public required string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Please Enter Your PhoneNumber")]
		[Phone(ErrorMessage = "Enter A Valid Email")]
		public required string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Please Enter Your Address")]
		public required string Address { get; set; }
        
    }
}
