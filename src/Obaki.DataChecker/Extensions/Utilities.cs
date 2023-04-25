using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obaki.DataChecker.Extensions
{
    internal static  class Utilities
    {
        internal static async Task<Stream> ToStreamAsync(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("The input string is null or empty.", nameof(str));
            }

            try
            {
                byte[] byteArray = await Task.Run(() => Encoding.UTF8.GetBytes(str));
                if (byteArray == null || byteArray.Length == 0)
                {
                    throw new Exception("Error converting string to stream: the byte array is null or empty.");
                }
                return new MemoryStream(byteArray);
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting string to stream.", ex);
            }
        }
    }
}
