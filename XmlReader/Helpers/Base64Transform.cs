using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Text;

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

        public static string ConvertBase64ToString(string content)
        {
            var bytes = Convert.FromBase64String(content);
            var xml = System.Text.Encoding.UTF8.GetString(bytes);
            return xml;
        }

        public static bool IsBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return false;

            base64 = base64.Trim();

            if (base64.Length % 4 != 0)
                return false;

            try
            {
                Convert.FromBase64String(base64);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
