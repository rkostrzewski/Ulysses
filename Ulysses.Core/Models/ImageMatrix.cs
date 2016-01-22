namespace Ulysses.Core.Models
{
    public abstract class ImageContainer
    {
        public ImageModel ImageModel { get; }

        protected ImageContainer(ImageModel imageModel)
        {
            ImageModel = imageModel;
        }
    }
}