namespace Vaccination.BussinessLogic.Commons.Constants
{
    public static class MessageCustomer
    {
        public const string FULLNAME_ERROR = "Full name is required and must not exceed 100 characters.";
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
        public const string ADD_SUCCESS = "Customer added successfully.";
        public const string UPDATE_SUCCESS = "Customer updated successfully.";
        public const string DELETE_SUCCESS = "Customer deleted successfully.";
        public const string SEARCH_SUCCESS = "Customer found successfully.";

        // Failure messages
        public const string ADD_FAILURE = "Failed to add customer.";
        public const string UPDATE_FAILURE = "Failed to update customer.";
        public const string DELETE_FAILURE = "Failed to delete customer.";
        public const string SEARCH_FAILURE = "Failed to find customer.";

        //
        public const string CUSTOMERS_NOT_FOUND = "No customers found.";
        public const string CUSTOMER_RETRIEVED_SUCCESS = "Customer retrieved successfully.";
        public const string CUSTOMER_NOT_FOUND = "Customer not found.";
        public const string INVALID_CUSTOMER_DATA = "Invalid customer data.";
        public const string CUSTOMER_ID_MISMATCH = "Customer ID mismatch.";
    }
}

