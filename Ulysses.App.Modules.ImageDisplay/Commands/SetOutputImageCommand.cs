using System;
using System.Windows.Media.Imaging;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Modules.ImageDisplay.Models;
using Ulysses.Core.Models;

namespace Ulysses.App.Modules.ImageDisplay.Commands
{
    public class SetOutputImageCommand : Command<Image>, ISetOutputImageCommand
    {
        private readonly IImageConverter _imageConverter;

        public SetOutputImageCommand(IImageConverter imageConverter)
        {
            _imageConverter = imageConverter;
        }

        public override void Execute(Image parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new CannotExecuteCommandException(GetType());
            }

            var bitmap = _imageConverter.ConvertToBitmapSource(parameter);
            OnImageUpdate?.Invoke(bitmap);
        }

        public Action<BitmapSource> OnImageUpdate { get; set; }

        public override bool CanExecute(Image parameter)
        {
            return OnImageUpdate != null;
        }
    }
}