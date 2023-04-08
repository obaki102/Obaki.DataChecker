using FluentValidation;
using FluentValidation.Results;
using Obaki.DataChecker.Interfaces;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Obaki.DataChecker.Services
{
    internal sealed class XmlDataChecker<T> : IXmlDataChecker<T> where T : class
    {
        private static readonly Regex xmlReservedCharacters = new Regex("[&%]");
        private readonly IValidator<T> _validator;

        public XmlDataChecker(IValidator<T> validator)
        {
            _validator = validator;
        }

        internal T? DeserializeInputString(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            string sanitized = xmlReservedCharacters.Replace(input, "");

            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StringReader(sanitized);

            return (T?)serializer.Deserialize(reader);

        }


        public ValidationResult ValidateXmlDataFromString(string input)
        {
            var objToValidate = DeserializeInputString(input);
            if (objToValidate is null)
                throw new ArgumentNullException(nameof(objToValidate));
         
            if (_validator is null)
                throw new ArgumentNullException($"No rules set up on type{typeof(T)}");

            return _validator.Validate(objToValidate);
        }

    }
}
