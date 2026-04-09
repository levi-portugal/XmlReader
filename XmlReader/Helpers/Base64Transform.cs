using System;
using System.Collections.Generic;
using System.Text;

namespace XmlReader.Helpers
{
    public class Base64Transform
    {
        public static string ConvertToBase64 (string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            var encodedData = Convert.ToBase64String(bytes);
            return encodedData;
        }
    }
}
