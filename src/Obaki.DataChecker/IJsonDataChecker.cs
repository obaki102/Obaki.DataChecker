using FluentValidation;
using FluentValidation.Results;

namespace Obaki.DataChecker
{
    public interface IJsonDataChecker<T>
    {
        ValidationResult ValidateJsonDataFromString(string input);
        ValidationResult ValidateJsonDataFromString(string input, IValidator<T> explicitValidator);
        Task<ValidationResult> ValidateJsonDataFromStringAsync(string input);
        Task<ValidationResult> ValidateJsonDataFromStringAsync(string input, IValidator<T> explicitValidator);
        T? DeserializeInputString(string input);
        Task<T?> DeserializeInputStringAsync(string input);
    }
}
