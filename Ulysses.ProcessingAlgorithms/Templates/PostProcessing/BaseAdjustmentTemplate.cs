namespace Ulysses.ProcessingAlgorithms.Templates.PostProcessing
{
    public abstract class BaseAdjustmentTemplate : BaseImageProcessingAlgorithmTemplate
    {
        public double AdjustmentValue { get; set; }

        public override ImageProcessingAlgorithmGroup Group { get; }
    }
}