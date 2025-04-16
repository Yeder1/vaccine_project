using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.BussinessLogic.Commons.Constants
{
    public class MessageEmployee
    {
        public const string EMPLOYEE_NAME_ERROR = "Employee name is required and must not exceed 100 characters.";
        public const string DATE_OF_BIRTH_ERROR = "Date of birth is required.";
        public const string GENDER_ERROR = "Gender is required.";
        public const string IDENTITY_CARD_ERROR = "Identity Card must be numeric and exactly 10 digits.";
        public const string NATIVE_PLACE_ERROR = "Native place is required and must not exceed 100 characters.";
        public const string USENAME_ERROR = "Username is required and must not exceed 10 characters.";
        public const string PASSWORD_ERROR = "Password is required and must not exceed 20 characters.";
        public const string CONFIRM_PASSWORD_ERROR = "Confirm password must match the password and must not exceed 20 characters.";
        public const string EMAIL_ERROR = "Email must be valid and not exceed 25 characters.";
        public const string PHONE_ERROR = "Phone number must be numeric and exactly 11 digits.";

        // Success messages
        public const string ADD_SUCCESS = "Employee added successfully.";
        public const string UPDATE_SUCCESS = "Employee updated successfully.";
        public const string DELETE_SUCCESS = "Employee deleted successfully.";
        public const string SEARCH_SUCCESS = "Employee found successfully.";

        // Failure messages
        public const string ADD_FAILURE = "Failed to add employee.";
        public const string UPDATE_FAILURE = "Failed to update employee.";
        public const string DELETE_FAILURE = "Failed to delete employee.";
        public const string SEARCH_FAILURE = "Failed to find employee.";

        //
        public const string EMPLOYEES_NOT_FOUND = "No employees found.";
        public const string EMPLOYEE_RETRIEVED_SUCCESS = "Employee retrieved successfully.";
        public const string EMPLOYEE_NOT_FOUND = "Employee not found.";
        public const string INVALID_EMPLOYEE_DATA = "Invalid employee data.";
        public const string EMPLOYEE_ID_MISMATCH = "Employee ID mismatch.";
    }
}
