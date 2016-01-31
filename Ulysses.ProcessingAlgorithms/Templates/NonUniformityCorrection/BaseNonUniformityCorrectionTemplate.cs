namespace Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection
{
    public abstract class BaseNonUniformityCorrectionTemplate : BaseImageProcessingAlgorithmTemplate
    {
        public override ImageProcessingAlgorithmGroup Group => ImageProcessingAlgorithmGroup.NonUniformityCorrection;

        public override string ElementName => AlgorithmType.Name;
    }
}