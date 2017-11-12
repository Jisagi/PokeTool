using System;
using System.Collections.Generic;
using System.IO;
using PokeTool.Objects;

namespace PokeTool.Handler
{
    class FileHandler
    {
        private string Path { get; }

        public FileHandler(string path)
        {
            Path = path;
        }

        public List<string> GetFileListWithoutExtensions()
        {
            try
            {
                var files = Directory.GetFiles(Path);
                var fileList = new List<string>();
                foreach (var file in files)
                {
                    fileList.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                }
                return fileList;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public List<string> GetCroFileList(Pokemon.Version version)
        {
            try
            {
                var files = Directory.GetFiles(Path, "*.cro");
                var fileList = new List<string>();

                switch (version)
                {
                    case Pokemon.Version.X:
                    case Pokemon.Version.Y:
                    case Pokemon.Version.OmegaRuby:
                    case Pokemon.Version.AlphaSaphire:
                        var affectedFilesGen6 = new List<string> { "DllField", "DllPoke3Select", "DllBattle" };
                        foreach (var file in files)
                        {
                            if (affectedFilesGen6.Contains(System.IO.Path.GetFileNameWithoutExtension(file))) fileList.Add(System.IO.Path.GetFileName(file));
                        }
                        break;
                    case Pokemon.Version.Sun:
                    case Pokemon.Version.Moon:
                    case Pokemon.Version.UltraSun:
                    case Pokemon.Version.UltraMoon:
                        var affectedFilesGen7 = new List<string> { "Shop" };
                        foreach (var file in files)
                        {
                            if (affectedFilesGen7.Contains(System.IO.Path.GetFileNameWithoutExtension(file))) fileList.Add(System.IO.Path.GetFileName(file));
                        }
                        break;
                }

                return fileList;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public List<string> GetCodeBinFile()
        {
            try
            {
                var files = Directory.GetFiles(Path, "*code*");
                var fileList = new List<string>();
                foreach (var file in files)
                {
                    fileList.Add(System.IO.Path.GetFileName(file));
                }
                return fileList;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
