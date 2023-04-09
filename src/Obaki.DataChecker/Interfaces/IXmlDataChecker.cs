using FluentValidation.Results;

namespace Obaki.DataChecker.Interfaces
{
    public interface IXmlDataChecker<T>
    {
        ValidationResult ValidateXmlDataFromString(string input);
        Task<ValidationResult> ValidateXmlDataFromStringAsync(string input);
        T? DeserializeInputString(string input);
    }
}
