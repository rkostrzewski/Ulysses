using System;
using System.Windows.Media.Imaging;
using Ulysses.App.Core.Commands;
using Ulysses.App.Modules.ImageDisplay.Models;
using Ulysses.Core.Models;

namespace Ulysses.App.Modules.ImageDisplay.Commands
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