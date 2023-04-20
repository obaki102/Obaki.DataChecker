using FluentValidation;
using FluentValidation.Results;

namespace Obaki.DataChecker.Interfaces
{
    public interface IXmlDataChecker<T>
    {
        ValidationResult ValidateXmlDataFromString(string input);
        ValidationResult ValidateXmlDataFromString(string input, IValidator<T> validator);
        Task<ValidationResult> ValidateXmlDataFromStringAsync(string input);
        T? DeserializeInputString(string input);
    }
}
