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
    public partial class PokeTool : Form
    {
        private readonly PathValidator _pathValidator = new PathValidator();
        private bool _checkBackup;
        private bool _checkRomFs;
        private string _pathBackup;
        private string _pathRomFs;

        public PokeTool()
        {
            InitializeComponent();

            cboGameSelection.SelectedIndex = 6;
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
                ChangeValidationState(lblBackupValidator, false, Pokemon.Validation.Backup);
                if (path != string.Empty) MessageBox.Show(this, @"Please make sure you selected the game path inside the backup folder of pk3ds!", @"Path not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChangeValidationState(lblBackupValidator, true, Pokemon.Validation.Backup);
            _pathBackup = path;
        }

        private void btnRomFs_Click(object sender, EventArgs e)
        {
            var path = _pathValidator.FolderSelector();
            if (!_pathValidator.CheckRomFsFolder(path))
            {
                ChangeValidationState(lblRomFsValidator, false, Pokemon.Validation.RomFs);
                if (path != string.Empty) MessageBox.Show(this, @"Please make sure you selected the correct romfs path!", @"Path not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChangeValidationState(lblRomFsValidator, true, Pokemon.Validation.RomFs);
            _pathRomFs = path;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // check if read
            var question = MessageBox.Show(this, $@"Start the copy process now?{Environment.NewLine}This might take a few seconds so please be patient.", @"Please choose", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
            // X:0, Y:1, OR:2, AS:3, Su:4, Mo:5, US: 6, UM: 7
            var selectedGame = (Pokemon.Version)cboGameSelection.SelectedIndex;
            var fileHandlerCro = new FileHandler(_pathRomFs);
            var croFiles = fileHandlerCro.GetCroFileList(selectedGame);

            if (croFiles.Count < 1)
            {
                MessageBox.Show(this, @"Error while parsing the .cro files!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            var copyResult = FileCopy.CopyAllNecessaryFiles(selectedGame, romFsFiles, _pathRomFs, croFiles, codeBin);
            if (copyResult) MessageBox.Show(this, @"Finished copying all files!", @"Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else MessageBox.Show(this, $@"Error while copying the files!{Environment.NewLine}Check error-log.txt for more information.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // reset if successful
            if (!copyResult) return;
            ChangeValidationState(lblBackupValidator, false, Pokemon.Validation.Backup);
            ChangeValidationState(lblRomFsValidator, false, Pokemon.Validation.RomFs);
        }

        private void ChangeValidationState(Label lbl, bool valid, Pokemon.Validation folder)
        {
            lbl.Text = valid ? "valid" : "invalid";
            lbl.ForeColor = valid ? Color.Green : Color.Red;

            switch (folder)
            {
                case Pokemon.Validation.Backup:
                    _checkBackup = valid;
                    break;
                case Pokemon.Validation.RomFs:
                    _checkRomFs = valid;
                    break;
            }

            if (!valid) btnStart.Enabled = false;
            if (_checkBackup && _checkRomFs) btnStart.Enabled = true;
        }
    }
}
