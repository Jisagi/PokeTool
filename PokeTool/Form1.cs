using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PokeTool.Handler;
using PokeTool.Objects;

namespace PokeTool
{
    public partial class Form1 : Form
    {
        private readonly PathValidator _pathValidator = new PathValidator();
        private bool _checkBackup = false;
        private bool _checkRomFs = false;
        private string _pathBackup;
        private string _pathRomFs;

        public Form1()
        {
            InitializeComponent();

            cboGameSelection.SelectedIndex = 0;
            new ToolTip().SetToolTip(cboGameSelection, "Please select the correct game, otherwise it will not work.");
            new ToolTip().SetToolTip(btnBackup, "Select the game folder inside the pk3ds backup folder.");
            new ToolTip().SetToolTip(btnRomFs, "Select the romfs folder from the extracted game files.");
            new ToolTip().SetToolTip(btnStart, "Start the copy process.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(this, $@"Please refer to the layeredfs guide for more information.{Environment.NewLine}Press Help to open the guide in your browser.", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, 0, "https://zetadesigns.github.io/randomizing-layeredfs.html");
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            var path = _pathValidator.FolderSelector();
            if (!_pathValidator.CheckBackupFolder(path))
            {
                lblBackupValidator.Text = @"invalid";
                lblBackupValidator.ForeColor = Color.Red;
                _checkBackup = false;
                btnStart.Enabled = false;
                if (path != string.Empty) MessageBox.Show(this, @"Please make sure you selected the game path inside the backup folder of pk3ds!", @"Path not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblBackupValidator.Text = @"valid";
            lblBackupValidator.ForeColor = Color.Green;
            _checkBackup = true;
            _pathBackup = path;
            if (_checkBackup && _checkRomFs) AllChecksPositive();
        }

        private void btnRomFs_Click(object sender, EventArgs e)
        {
            var path = _pathValidator.FolderSelector();
            if (!_pathValidator.CheckRomFsFolder(path))
            {
                lblRomFsValidator.Text = @"invalid";
                lblRomFsValidator.ForeColor = Color.Red;
                _checkRomFs = false;
                btnStart.Enabled = false;
                if (path != string.Empty) MessageBox.Show(this, @"Please make sure you selected the correct romfs path!", @"Path not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblRomFsValidator.Text = @"valid";
            lblRomFsValidator.ForeColor = Color.Green;
            _checkRomFs = true;
            _pathRomFs = path;
            if (_checkBackup && _checkRomFs) AllChecksPositive();
        }

        private void AllChecksPositive()
        {
            btnStart.Enabled = true;
            // add other useful code here
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // check if read
            var question = MessageBox.Show(this, $@"Start the process of copying the necessary files?{Environment.NewLine}This might take a few seconds so please be patient.", @"Please choose", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (question != DialogResult.OK) return;

            // get files in backup folder
            var fileHandlerBackup = new FileHandler(Path.Combine(_pathBackup, "a"));
            var fileListBackup = fileHandlerBackup.GetFileListWithoutExtensions();
            if (fileListBackup.Count < 1)
            {
                MessageBox.Show(this, @"Error while parsing the backup folder!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // file -> RomFsFile
            var romFsFiles = new List<RomFsFile>();
            foreach (var file in fileListBackup)
            {
                romFsFiles.Add(new RomFsFile(file));
            }

            // get cro files
            var croFiles = new List<string>();
            var croFilesCheck = false;
            // OR:0, AS:1, X:2, Y:3, Su:4, Mo:5
            if (cboGameSelection.SelectedIndex != 4 && cboGameSelection.SelectedIndex != 5)
            {
                var fileHandlerCro = new FileHandler(_pathRomFs);
                var fileListCro = fileHandlerCro.GetCroFileList();
                if (fileListCro.Count < 1)
                {
                    MessageBox.Show(this, @"Error while parsing the .cro files!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                croFiles = fileListCro;
                croFilesCheck = true;
            }

            // get code.bin
            var exefsFolderPath = Path.Combine(Directory.GetParent(_pathRomFs).FullName, "exefs");
            var fileHandlerCodeBin = new FileHandler(exefsFolderPath);
            var fileListCodeBin = fileHandlerCodeBin.GetCodeBinFile();
            if (fileListCodeBin.Count < 1)
            {
                MessageBox.Show(this, @"Could not find code.bin in exefs folder!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var codeBin = fileListCodeBin.First();

            // copy process
            var copyResult = FileCopy.CopyAllNecessaryFiles(cboGameSelection.SelectedIndex, romFsFiles, _pathRomFs, croFilesCheck, croFiles, codeBin);
            if (copyResult) MessageBox.Show(this, @"Finished copying all files!", @"Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else MessageBox.Show(this, $@"Error while copying the files!{Environment.NewLine}Check error-log.txt for more information.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
