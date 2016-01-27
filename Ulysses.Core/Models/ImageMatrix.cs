namespace Ulysses.Core.Models
{
    public abstract class ImageContainer
    {
        protected ImageContainer(ImageModel imageModel)
        {
            ImageModel = imageModel;
        }

        public ImageModel ImageModel { get; }
    }
}