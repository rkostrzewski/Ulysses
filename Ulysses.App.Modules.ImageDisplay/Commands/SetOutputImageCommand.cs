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
        private readonly IImageConverter _imageConverter;

        public SetOutputImageCommand(Action<BitmapSource> setOutputImageDelegate, IImageConverter imageConverter)
        {
            _setOutputImageDelegate = setOutputImageDelegate;
            _imageConverter = imageConverter;
        }

        public override void Execute(Image parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException();
            }

            var bitmap = _imageConverter.ConvertToBitmapSource(parameter);
            _setOutputImageDelegate.Invoke(bitmap);
        }

        public override bool CanExecute(Image parameter)
        {
            return _setOutputImageDelegate != null;
        }
    }
}