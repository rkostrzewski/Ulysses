using System;
using System.Windows.Media.Imaging;
using Ulysses.App.Modules.Content.ImageDisplay.Models;
using Ulysses.App.Utils;
using Ulysses.App.Utils.Commands;
using Ulysses.Core.Models;
using Ulysses.ProcessingEngine.Output;

namespace Ulysses.App.Modules.Content.ImageDisplay.Commands
{
    public class SetOutputImageCommand : Command<Image>, ISetOutputImageCommand
    {
        private readonly Action<BitmapSource> _setOutputImageDelegate;

        public SetOutputImageCommand(Action<BitmapSource> setOutputImageDelegate)
        {
            _setOutputImageDelegate = setOutputImageDelegate;
        }

        public override void Execute(Image parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException();
            }

            var bitmap = ImageConverter.ConvertToBitmapSource(parameter);
            _setOutputImageDelegate.Invoke(bitmap);
        }

        public override bool CanExecute(Image parameter)
        {
            return _setOutputImageDelegate != null;
        }
    }
}