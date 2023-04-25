using FluentValidation;
using FluentValidation.Results;
using Obaki.DataChecker.Extensions;
using System.Text.Json;

namespace Obaki.DataChecker
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
                return JsonSerializer.Deserialize<T?>(input);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Error deserializing JSON string.", ex);
            }
        }

        public async Task<T?> DeserializeInputStringAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string is null or empty.");
            }

            try
            {
                var streamValue = await input.ToStreamAsync() ?? throw new ArgumentNullException(nameof(input), "Input string is null or empty.");
                return await JsonSerializer.DeserializeAsync<T?>(streamValue);
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

        public ValidationResult ValidateJsonDataFromString(string input, IValidator<T> explicitValidator)
        {
            if (explicitValidator is null)
            {
                throw new ArgumentNullException(nameof(input), "Validator is null. Please define a validator");
            }

            var objToValidate = DeserializeInputString(input);
            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return explicitValidator.Validate(objToValidate);
        }

        public async Task<ValidationResult> ValidateJsonDataFromStringAsync(string input)
        {
            var objToValidate = await DeserializeInputStringAsync(input);
            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return await _validator.ValidateAsync(objToValidate);
        }

        public async Task<ValidationResult> ValidateJsonDataFromStringAsync(string input, IValidator<T> explicitValidator)
        {
            if (explicitValidator is null)
            {
                throw new ArgumentNullException(nameof(input), "Validator is null. Please define a validator");
            }

            var objToValidate = await DeserializeInputStringAsync(input);
            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return await explicitValidator.ValidateAsync(objToValidate);
        }
    }
}
