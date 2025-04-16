using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.Commons.Exceptions;
using Vaccination.BussinessLogic.DTOs.CustomerDTOs;

namespace Vaccination.BussinessLogic.Commons.Validations
{
    public class Validation
    {
        public static void ValidateCustomer(CustomerDTO customer)
        {
            List<string> errorMessages = new List<string>();


            if (string.IsNullOrEmpty(customer.FullName) || customer.FullName.Length > 100)
            {
                errorMessages.Add(MessageCustomer.FULLNAME_ERROR);
            }

            if (customer.DateOfBirth == null)
            {
                errorMessages.Add(MessageCustomer.DATE_OF_BIRTH_ERROR);
            }

            if (customer.Gender == null)
            {
                errorMessages.Add(MessageCustomer.GENDER_ERROR);
            }

            if (string.IsNullOrEmpty(customer.IdentityCard) || customer.IdentityCard.Length != 10 || !System.Text.RegularExpressions.Regex.IsMatch(customer.IdentityCard, @"^\d+$"))
            {
                errorMessages.Add(MessageCustomer.IDENTITY_CARD_ERROR);
            }

            if (string.IsNullOrEmpty(customer.Address) || customer.Address.Length > 100)
            {
                errorMessages.Add(MessageCustomer.NATIVE_PLACE_ERROR);
            }

            if (string.IsNullOrEmpty(customer.Username) || customer.Username.Length > 10)
            {
                errorMessages.Add(MessageCustomer.USENAME_ERROR);
            }

            if (string.IsNullOrEmpty(customer.Password) || customer.Password.Length > 20)
            {
                errorMessages.Add(MessageCustomer.PASSWORD_ERROR);
            }

            if (string.IsNullOrEmpty(customer.ConfirmPassword) || customer.ConfirmPassword.Length > 20 || customer.ConfirmPassword != customer.Password)
            {
                errorMessages.Add(MessageCustomer.CONFIRM_PASSWORD_ERROR);
            }

            if (string.IsNullOrEmpty(customer.Email) || customer.Email.Length > 25 || !System.Text.RegularExpressions.Regex.IsMatch(customer.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessages.Add(MessageCustomer.EMAIL_ERROR);
            }

            if (string.IsNullOrEmpty(customer.Phone) || customer.Phone.Length != 11 || !System.Text.RegularExpressions.Regex.IsMatch(customer.Phone, @"^\d+$"))
            {
                errorMessages.Add(MessageCustomer.PHONE_ERROR);
            }

            if (errorMessages.Count > 0)
            {
                throw new CustomerValidationException(errorMessages);
            }
        }
    }
}
