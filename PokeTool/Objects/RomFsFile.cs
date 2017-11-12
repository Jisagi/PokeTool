using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PokeTool.Objects
{
    class RomFsFile
    {
        public string TopFolder { get; }
        public string FirstFolder { get; }
        public string SecondFolder { get; }
        public string Filename { get; }

        public RomFsFile(string filename)
        {
            var split = GetFilenameWithPath(filename);
            TopFolder = split[0];
            FirstFolder = split[1];
            SecondFolder = split[2];
            Filename = split[3];
        }

        public string GetDirectory(string romfsPath)
        {
            return Path.Combine(new string[] { romfsPath, TopFolder, FirstFolder, SecondFolder });
        }

        public string GetFullFilePath(string romfsPath)
        {
            return Path.Combine(new string[] { GetDirectory(romfsPath), Filename });
        }

        private string[] GetFilenameWithPath(string filenameFull)
        {
            // 1: (.+?)( )(.*)
            // 2: (.)(\w)(\d{3})(.)
            var matchFirst = Regex.Match(filenameFull, "(.+?)( )(.*)");
            var locationWithBrackets = matchFirst.Groups[3].ToString();

            var matchSecond = Regex.Match(locationWithBrackets, "(.)(\\w)(\\d{3})(.)");
            var topFolder = matchSecond.Groups[2].ToString();
            var location = matchSecond.Groups[3].ToString();

            var split = location.ToCharArray();
            var final = split.Select(c => c.ToString()).ToArray();
            return new string[] { topFolder, final[0], final[1], final[2] };
        }
    }
}
