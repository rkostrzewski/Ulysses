using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Events;
using Ulysses.App.Core.Commands;
using Ulysses.App.Core.Events;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.ImageProviders.Factories;
using Ulysses.ImageProviders.Templates;
using Ulysses.ProcessingAlgorithms.Factories;
using Ulysses.ProcessingAlgorithms.Templates;
using Ulysses.ProcessingEngine.ProcessingEngine;
using Ulysses.ProcessingEngine.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Commands
{
    public class UpdateProcessingEngineCommand : NoParameterCommand, IUpdateProcessingEngineCommand
    {
        private readonly IImageProcessingChainFactory _imageProcessingChainFactory;
        private readonly IImageProviderFactory _imageProviderFactory;
        private readonly IProcessingChainBuilderDataStore _processingChainBuilderDataStore;
        private readonly ShouldUpdateProcessingEngineEvent _shouldUpdateProcessingEngineEvent;

        public UpdateProcessingEngineCommand(IEventAggregator eventAggregator,
                                             IProcessingChainBuilderDataStore processingChainBuilderDataStore,
                                             IImageProviderFactory imageProviderFactory,
                                             IImageProcessingChainFactory imageProcessingChainFactory)
        {
            _imageProviderFactory = imageProviderFactory;
            _imageProcessingChainFactory = imageProcessingChainFactory;
            _processingChainBuilderDataStore = processingChainBuilderDataStore;
            _shouldUpdateProcessingEngineEvent = eventAggregator.GetEvent<ShouldUpdateProcessingEngineEvent>();
        }

        public override bool CanExecute()
        {
            return _shouldUpdateProcessingEngineEvent != null && _imageProviderFactory != null && _imageProcessingChainFactory != null &&
                   _processingChainBuilderDataStore != null && _processingChainBuilderDataStore.ProcessingChainTemplate.IsValid();
        }

        public override void Execute()
        {
            if (!CanExecute())
            {
                throw new CannotExecuteCommandException(GetType());
            }

            var processingChainTemplate = _processingChainBuilderDataStore.ProcessingChainTemplate;

            var imageProviderTemplate = GetImageProviderTemplate(processingChainTemplate);
            var imageProcessingChainAlgorithmsTemplates = GetImageProcessingChainAlgorithmsTemplates(processingChainTemplate);

            var imageProvider = _imageProviderFactory.CreateInstance(imageProviderTemplate);
            var imageProcessingChain = _imageProcessingChainFactory.BuildChain(imageProcessingChainAlgorithmsTemplates, imageProviderTemplate.ImageModel);

            var processingEngineTemplate = new ProcessingEngineTemplate
            {
                ImageProvider = imageProvider,
                ImageProcessingChain = imageProcessingChain,
                ProcessingStrategy = ProcessingStrategy.Sync
            };

            _shouldUpdateProcessingEngineEvent.Publish(processingEngineTemplate);
        }

        private static IEnumerable<IImageProcessingAlgorithmTemplate> GetImageProcessingChainAlgorithmsTemplates(ProcessingChainTemplate processingChainTemplate)
        {
            var middleChainElements = processingChainTemplate.Skip(1).Reverse().Skip(1).Reverse().ToList();

            if (middleChainElements.Any(e => !(e is IImageProcessingAlgorithmTemplate)))
            {
                throw new InvalidCastException();
            }

            return middleChainElements.Cast<IImageProcessingAlgorithmTemplate>();
        }

        private static ImageProviderTemplate GetImageProviderTemplate(ProcessingChainTemplate processingChainTemplate)
        {
            var firstChainElement = processingChainTemplate.First() as ImageProviderTemplate;
            if (firstChainElement == null)
            {
                throw new InvalidCastException();
            }

            return firstChainElement;
        }
    }
}