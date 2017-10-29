using System;
using System.Collections.Generic;
using System.IO;

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

        public List<string> GetCroFileList()
        {
            try
            {
                var files = Directory.GetFiles(Path, "*.cro");
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
