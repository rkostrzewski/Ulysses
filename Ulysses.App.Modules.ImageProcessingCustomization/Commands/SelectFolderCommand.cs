using System;
using System.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Exceptions;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public class SelectFolderCommand : NoParameterCommand, ISelectFolderCommand
    {
        public override bool CanExecute()
        {
            return OnFolderSelected != null;
        }

        public override void Execute()
        {
            if (!CanExecute())
            {
                throw new CannotExecuteCommandException(GetType());
            }

            var openFolderDialog = new CommonOpenFileDialog { IsFolderPicker = true, EnsurePathExists = true, Multiselect = false };
            var result = openFolderDialog.ShowDialog();

            if (result != CommonFileDialogResult.Ok)
            {
                return;
            }

            var directoryPath = openFolderDialog.FileNames.Single();
            OnFolderSelected?.Invoke(directoryPath);
        }

        public Action<string> OnFolderSelected { get; set; }
    }
}