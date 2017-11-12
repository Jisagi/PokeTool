using System;
using System.Collections.Generic;
using System.IO;
using PokeTool.Objects;

namespace PokeTool.Handler
{
    static class FileCopy
    {
        public static bool CopyAllNecessaryFiles(Pokemon.Version game, List<RomFsFile> files, string pathRomFs, List<string> croFiles, string codeBin)
        {
            try
            {
                var appLocation = AppDomain.CurrentDomain.BaseDirectory;
                var titleIdPath = Path.Combine(new string[] { appLocation, GetTitleId(game) });
                var titleIdPathRomFs = Path.Combine(new string[] { titleIdPath, "romfs" });
                var titleIdPathExeFs = Path.Combine(Directory.GetParent(pathRomFs).FullName, "exefs");

                foreach (var romFsFile in files)
                {
                    var targetFolder = romFsFile.GetDirectory(titleIdPathRomFs);
                    if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);
                    var origFilePath = romFsFile.GetFullFilePath(pathRomFs);
                    var newFilePath = romFsFile.GetFullFilePath(titleIdPathRomFs);
                    File.Copy(origFilePath, newFilePath, true);
                }

                // copy cro files
                foreach (var croFile in croFiles)
                {
                    var origFilePath = Path.Combine(pathRomFs, croFile);
                    var newFilePath = Path.Combine(titleIdPathRomFs, croFile);
                    File.Copy(origFilePath, newFilePath, true);
                }

                // copy and rename to 'code.bin'
                var origCodeBin = Path.Combine(titleIdPathExeFs, codeBin);
                var newCodeBin = Path.Combine(titleIdPath, "code.bin");
                File.Copy(origCodeBin, newCodeBin, true);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                return false;
            }
        }

        private static string GetTitleId(Pokemon.Version game)
        {
            switch (game)
            {
                case Pokemon.Version.X:
                    return "000400000011C400";
                case Pokemon.Version.Y:
                    return "000400000011C500";
                case Pokemon.Version.OmegaRuby:
                    return "0004000000055D00";
                case Pokemon.Version.AlphaSaphire:
                    return "0004000000055E00";
                case Pokemon.Version.Sun:
                    return "0004000000164800";
                case Pokemon.Version.Moon:
                    return "0004000000175E00";
                case Pokemon.Version.UltraSun:
                    return "00040000001B5000";
                case Pokemon.Version.UltraMoon:
                    return "00040000001B5100";
                default:
                    return "titleID";
            }
        }
    }
}
