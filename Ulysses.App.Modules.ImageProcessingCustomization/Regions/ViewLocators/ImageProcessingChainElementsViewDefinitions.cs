﻿using System;
using System.Collections.Generic;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.ImageProvider;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.NonUniformityCorrection;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.PostProcessing;
using Ulysses.App.Modules.ImageProcessingCustomization.Views.TemplateViews.Utilities;
using Ulysses.ImageProviders.Templates;
using Ulysses.ProcessingAlgorithms.Templates.DummyAlgorithms;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;
using Ulysses.ProcessingAlgorithms.Templates.PostProcessing;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Regions.ViewLocators
{
    public static class ImageProcessingChainElementsViewDefinitions
    {
        public static IDictionary<Type, Type> ViewDefinitions { get; } = new Dictionary<Type, Type>
        {
            { typeof (ImageProviderTemplate), typeof (ImageProviderCustomizationView) },
            { typeof (TwoPointNonUniformityCorrectionTemplate), typeof (TwoPointNonUniformityCorrectionCustomizationView) },
            { typeof (ConstantRangeNonUniformityCorrectionTemplate), typeof (ConstantRangeNonUniformityCorrectionCustomizationView) },
            { typeof (BrightnessAdjustmentTemplate), typeof (BrightnessAdjustmentCustomizationView) },
            { typeof (ContrastAdjustmentTemplate), typeof (ContrastAdjustmentCustomizationView) },
            { typeof (GammaAdjustmentTemplate), typeof (GammaAdjustmentCustomizationView) },
            { typeof (SleeperTemplate), typeof (SleeperCustomizationView) }
        };
    }
}