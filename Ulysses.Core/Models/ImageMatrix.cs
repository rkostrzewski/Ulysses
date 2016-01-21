namespace Ulysses.Core.Models
{
    public abstract class ImageContainer
    {
        public readonly ImageModel ImageModel;

        protected ImageContainer(ushort height, ushort width, ImageBitDepth bitDepth)
        {
            ImageModel = new ImageModel(height, width, bitDepth);
        }

        protected ImageContainer(ImageModel imageModel)
        {
            ImageModel = imageModel;
        }
    }
}