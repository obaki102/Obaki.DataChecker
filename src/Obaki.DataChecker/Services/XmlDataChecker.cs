using FluentValidation;
using FluentValidation.Results;
using Obaki.DataChecker.Interfaces;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Obaki.DataChecker.Services
{
    internal sealed class XmlDataChecker<T> : IXmlDataChecker<T> where T : class
    {
        private const string XmlReservedCharactersPattern = "[&%]";
        private readonly IValidator<T> _validator;

        public XmlDataChecker(IValidator<T> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator), $"No rules set up for validator of type {typeof(T)}.");
        }

        public T? DeserializeInputString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string is null or empty.");
            }

            string sanitized = Regex.Replace(input, XmlReservedCharactersPattern, "");
            using var reader = XmlReader.Create(new StringReader(sanitized));
            var serializer = new XmlSerializer(typeof(T));

            try
            {
                return (T?)serializer.Deserialize(reader);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Error deserializing XML string.", ex);
            }
        }

        public ValidationResult ValidateXmlDataFromString(string input)
        {
            var objToValidate = DeserializeInputString(input);

            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return _validator.Validate(objToValidate);
        }

        public ValidationResult ValidateXmlDataFromString(string input, IValidator<T> validator)
        {
            if (validator is null)
            {
                throw new ArgumentNullException(nameof(input), "Validator is null. Please define a validator");
            }

            var objToValidate = DeserializeInputString(input);

            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return validator.Validate(objToValidate);
        }

        public async Task<ValidationResult> ValidateXmlDataFromStringAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string is null or empty.");
            }

            var objToValidate = DeserializeInputString(input);

            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return await _validator.ValidateAsync(objToValidate);
        }

        public async Task<ValidationResult> ValidateXmlDataFromStringAsync(string input, IValidator<T> validator)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string is null or empty.");
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(input), "Validator is null. Please define a validator");
            }

            var objToValidate = DeserializeInputString(input);

            if (objToValidate is null)
            {
                throw new ArgumentNullException(nameof(objToValidate), "Deserialized object is null.");
            }

            return await validator.ValidateAsync(objToValidate);
        }
    }

}
