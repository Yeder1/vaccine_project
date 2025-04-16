namespace Vaccination.BussinessLogic.Commons.Exceptions
{
    public class CustomerValidationException : Exception
    {
        public List<string> Errors { get; }

        public CustomerValidationException(List<string> errors)
        {
            this.Errors = errors;
        }


        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors);
        }
    }
}
