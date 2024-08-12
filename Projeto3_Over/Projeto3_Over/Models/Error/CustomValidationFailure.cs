namespace Projeto3_Over.Models.Error
{
    public class CustomValidationFailure
    {
        public CustomValidationFailure(string property, string errorMessage)
        {
            Property = property;
            ErrorMessage = errorMessage;
        }

        public string Property { get; set; }
        public string ErrorMessage { get; set; }
    }
}
