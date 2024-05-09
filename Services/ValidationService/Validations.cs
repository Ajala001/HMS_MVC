using System.Text.RegularExpressions;

namespace HMSMVC.Services.ValidationService
{
    public class Validations
    {
        public static bool ValidateEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@]+@(gmail|hotmail|yahoo|outlook|aol|prodigy|comcast|yahoo|verizon|icloud|me|mac|live|msn|google|facebook|twitter)+\.(com|net|org|edu|gov|mil|biz|info|mobi|tel|tv|asia|coop|int|jobs|museum|name|pro|tel|travel|xxx)$");
            return emailRegex.IsMatch(email);
        }

        public static bool ValidatePassword(string password)
        {
            var passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
            return passwordRegex.IsMatch(password);
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            var phoneRegex = new Regex(@"^(\+234|0)(8|7)(0|1|5|6|7|8|9)(\d{8})$");
            return phoneRegex.IsMatch(phoneNumber);
        }

        public static bool ValidatePin(string pin)
        {
            var pinRegex = new Regex(@"^\d{4}$");
            return pinRegex.IsMatch(pin);
        }

    }
}
