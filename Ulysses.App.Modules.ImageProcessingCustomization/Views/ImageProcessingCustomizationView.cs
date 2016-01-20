﻿using Ulysses.App.Modules.ImageProcessingCustomization.ViewModels;

namespace Ulysses.App.Modules.ImageProcessingCustomization.Views
{
    public partial class ImageProcessingCustomizationView
    {
        public ImageProcessingCustomizationView(IImageProcessingCustomizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}