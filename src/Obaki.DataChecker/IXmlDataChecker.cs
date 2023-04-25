using FluentValidation;
using FluentValidation.Results;

namespace Obaki.DataChecker
{
    public interface IXmlDataChecker<T>
    {
        ValidationResult ValidateXmlDataFromString(string input);
        ValidationResult ValidateXmlDataFromString(string input, IValidator<T> explicitValidator);
        Task<ValidationResult> ValidateXmlDataFromStringAsync(string input);
        Task<ValidationResult> ValidateXmlDataFromStringAsync(string input, IValidator<T> explicitValidator);
        T? DeserializeInputString(string input);
    }
}
