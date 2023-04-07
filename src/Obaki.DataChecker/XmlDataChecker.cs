using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Obaki.DataChecker
{
    internal sealed class XmlDataChecker : IDataChecker
    {
        private static readonly Regex xmlReservedCharacters = new Regex("[&%]");

        public T? DeserializeInputString<T>(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            string sanitized =  xmlReservedCharacters.Replace(input, "");

            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StringReader(sanitized);

            return (T?)serializer.Deserialize(reader);

        }

        public bool ValidateData()
        {
            throw new NotImplementedException();
        }
    }
}
