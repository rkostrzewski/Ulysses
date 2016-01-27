using System;
using System.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Exceptions;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public class SelectFileCommand : NoParameterCommand, ISelectFileCommand
    {
        public override bool CanExecute()
        {
            return OnFileSelected != null;
        }

        public override void Execute()
        {
            if (!CanExecute())
            {
                throw new CannotExecuteCommandException(GetType());
            }

            var openFolderDialog = new CommonOpenFileDialog { IsFolderPicker = false, EnsurePathExists = true, EnsureFileExists = true, Multiselect = false };
            var result = openFolderDialog.ShowDialog();

            if (result != CommonFileDialogResult.Ok)
            {
                return;
            }

            var directoryPath = openFolderDialog.FileNames.Single();
            OnFileSelected?.Invoke(directoryPath);
        }

        public Action<string> OnFileSelected { get; set; }
    }
}