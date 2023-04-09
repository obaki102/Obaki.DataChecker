using FluentValidation;
using FluentValidation.Results;
using Obaki.DataChecker.Interfaces;
using System.Text.Json;

namespace Obaki.DataChecker.Services
{
    internal sealed class JsonDataChecker<T> : IJsonDataChecker<T> where T : class
    {
        private readonly IValidator<T> _validator;
        public JsonDataChecker(IValidator<T> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator), $"No rules set up for validator of type {typeof(T)}.");
        }

        public T? DeserializeInputString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string is null or empty.");
            }

            try
            {
                return JsonSerializer.Deserialize<T>(input);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Error deserializing JSON string.", ex);
            }
        }

        public ValidationResult ValidateJsonDataFromString(string input)
        {
            var objToValidate = DeserializeInputString(input);
            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return _validator.Validate(objToValidate);
        }

        public async Task<ValidationResult> ValidateJsonDataFromStringAsync(string input)
        {
            var objToValidate = DeserializeInputString(input);
            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return await _validator.ValidateAsync(objToValidate);
        }

    }
}
