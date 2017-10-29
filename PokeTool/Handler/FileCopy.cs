using System;
using System.Collections.Generic;
using System.IO;
using PokeTool.Objects;

namespace PokeTool.Handler
{
    static class FileCopy
    {
        public static bool CopyAllNecessaryFiles(int game, List<RomFsFile> files, string pathRomFs, bool croFilesCheck, List<string> croFiles, string codeBin)
        {
            try
            {
                var appLocation = AppDomain.CurrentDomain.BaseDirectory;
                var titleIdPath = Path.Combine(new string[] { appLocation, GetTitleId(game) }); // change to actual id
                var titleIdPathRomFs = Path.Combine(new string[] { appLocation, GetTitleId(game), "romfs" });  // change to actual id
                var titleIdPathExeFs = Path.Combine(Directory.GetParent(pathRomFs).FullName, "exefs");
                if (!Directory.Exists(titleIdPathRomFs)) Directory.CreateDirectory(titleIdPathRomFs);

                foreach (var romFsFile in files)
                {
                    var origFilePath = romFsFile.GetFullFilePath(pathRomFs);
                    var newFilePath = romFsFile.GetFullFilePath(titleIdPathRomFs);
                    if (!Directory.Exists(Path.Combine(titleIdPathRomFs, romFsFile.TopFolder))) Directory.CreateDirectory(Path.Combine(titleIdPathRomFs, romFsFile.TopFolder));
                    if (!Directory.Exists(Path.Combine(new string[] { titleIdPathRomFs, romFsFile.TopFolder, romFsFile.FirstFolder }))) Directory.CreateDirectory(Path.Combine(new string[] { titleIdPathRomFs, romFsFile.TopFolder, romFsFile.FirstFolder }));
                    if (!Directory.Exists(Path.Combine(new string[] { titleIdPathRomFs, romFsFile.TopFolder, romFsFile.FirstFolder, romFsFile.SecondFolder }))) Directory.CreateDirectory(Path.Combine(new string[] { titleIdPathRomFs, romFsFile.TopFolder, romFsFile.FirstFolder, romFsFile.SecondFolder }));
                    File.Copy(origFilePath, newFilePath, true);
                }

                // copy cro files if necessary
                if (croFilesCheck)
                {
                    foreach (var croFile in croFiles)
                    {
                        var origFilePath = Path.Combine(pathRomFs, croFile);
                        var newFilePath = Path.Combine(titleIdPathRomFs, croFile);
                        File.Copy(origFilePath, newFilePath, true);
                    }
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

        private static string GetTitleId(int game)
        {
            switch (game)
            {
                case 0:
                    return "000400000011C400";
                case 1:
                    return "000400000011C500";
                case 2:
                    return "0004000000055D00";
                case 3:
                    return "0004000000055E00";
                case 4:
                    return "0004000000164800";
                case 5:
                    return "0004000000175E00";
                default:
                    return "titleID";
            }
        }
    }
}
