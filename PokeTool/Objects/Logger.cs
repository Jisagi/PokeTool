using System.IO;

namespace PokeTool.Objects
{
    static class Logger
    {
        public static void Log(string text)
        {
            using (var w = File.AppendText("error-log.txt"))
            {
                w.WriteLine(text);
            }
        }
    }
}
