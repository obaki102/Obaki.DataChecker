namespace Obaki.DataChecker
{
    public interface IDataChecker
    {
        T? DeserializeInputString<T>(string input);

        bool ValidateData();
    }
}
