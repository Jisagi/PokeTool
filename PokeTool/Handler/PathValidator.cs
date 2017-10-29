using System;
using System.IO;
using System.Windows.Forms;

namespace PokeTool.Handler
{
    class PathValidator
    {
        public string FolderSelector()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            return folderBrowserDialog.ShowDialog(null) != DialogResult.OK ? string.Empty : folderBrowserDialog.SelectedPath;
        }

        public bool CheckBackupFolder(string path)
        {
            try
            {
                var fullPath = Path.GetFullPath(path);
                var checkA = Directory.Exists(Path.Combine(fullPath, "a"));
                var checkExefs = Directory.Exists(Path.Combine(fullPath, "exefs"));
                return checkA && checkExefs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckRomFsFolder(string path)
        {
            try
            {
                var fullPath = Path.GetFullPath(path);
                var checkA = Directory.Exists(Path.Combine(fullPath, "a"));
                return checkA;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
