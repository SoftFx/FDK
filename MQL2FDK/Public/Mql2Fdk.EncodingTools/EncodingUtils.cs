namespace Mql2Fdk.EncodingTools
{
    using System.IO;

    public static class EncodingUtils
    {
        public static string ReadFileContent(this string fileName)
        {
            // Create a StreamReader with the guessed best encoding
            using (var sr = EncodingDetection.OpenTextFile(fileName))
            {
                return sr.ReadToEnd();
            }
        }

        public static void WriteFileContent(this string fileName, string content)
        {
            var encoding = EncodingDetection.DetectOutgoingEncoding(content);
            File.WriteAllText(fileName, content, encoding);
        }
    }
}
