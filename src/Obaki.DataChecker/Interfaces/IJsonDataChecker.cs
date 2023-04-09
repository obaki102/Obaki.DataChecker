using FluentValidation.Results;

namespace Obaki.DataChecker.Interfaces
{
    public interface IJsonDataChecker<T>
    {
        ValidationResult ValidateJsonDataFromString(string input);
        Task<ValidationResult> ValidateJsonDataFromStringAsync(string input);
        T? DeserializeInputString(string input);
    }
}
