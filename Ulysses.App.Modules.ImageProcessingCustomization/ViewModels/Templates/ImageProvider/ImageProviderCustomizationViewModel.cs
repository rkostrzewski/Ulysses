using System.Collections.Generic;
using System.Linq;
using Ulysses.App.Core.Providers;
using Ulysses.App.Modules.ImageProcessingCustomization.Commands;
using Ulysses.App.Modules.ImageProcessingCustomization.Models.DataStore;
using Ulysses.App.Resources;
using Ulysses.Core.Models;
using Ulysses.ImageProviders;
using Ulysses.ImageProviders.Templates;

namespace Ulysses.App.Modules.ImageProcessingCustomization.ViewModels.Templates.ImageProvider
{
    public class ImageProviderCustomizationViewModel : BaseImageProcessingChainElementCustomizationViewModel<ImageProviderTemplate>, IImageProviderCustomizationViewModel
    {
        private int _selectedIndex;

        public ImageProviderCustomizationViewModel(IProcessingChainBuilderDataStore dataStore, ISelectFolderCommand selectFolderCommand) : base(dataStore)
        {
            SelectFolderCommand = selectFolderCommand;
            AvailableBitDepths = new DisplayableEnumProvider<ImageBitDepth>(ImageBitDepthResources.ResourceManager).GetDisplayableEnums().ToList();
            AvailableImageProviders = new DisplayableEnumProvider<ImageProviderType>(ImageProviderResources.ResourceManager).GetDisplayableEnums().ToList();

            if (ChainElement == null)
            {
                ChainElement = new ImageProviderTemplate();
            }

            SelectFolderCommand.OnFolderSelected = selectedFolder => FolderPath = selectedFolder;
            SelectedTabIndex = (int)SelectedImageProvider;
        }

        public ISelectFolderCommand SelectFolderCommand { get; }

        public IList<Displayable<ImageProviderType>> AvailableImageProviders { get; }

        public int SelectedTabIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public ImageProviderType SelectedImageProvider
        {
            get
            {
                return ChainElement.ImageProviderType;
            }
            set
            {
                if (ChainElement.ImageProviderType == value)
                {
                    return;
                }

                ChainElement.ImageProviderType = value;
                SelectedTabIndex = (int)value;
                OnPropertyChanged();
            }
        }

        public ImageBitDepth ImageBitDepth
        {
            get
            {
                return ChainElement.ImageModel.ImageBitDepth;
            }
            set
            {
                if (ChainElement.ImageModel.ImageBitDepth == value)
                {
                    return;
                }

                ChainElement.ImageModel = new ImageModel(ChainElement.ImageModel.Width, ChainElement.ImageModel.Height, value);
                OnPropertyChanged();
            }
        }

        public ushort ImageWidth
        {
            get
            {
                return ChainElement.ImageModel.Width;
            }
            set
            {
                if (ChainElement.ImageModel.Width == value)
                {
                    return;
                }

                ChainElement.ImageModel = new ImageModel(value, ChainElement.ImageModel.Height, ChainElement.ImageModel.ImageBitDepth);
                OnPropertyChanged();
            }
        }

        public ushort ImageHeight
        {
            get
            {
                return ChainElement.ImageModel.Height;
            }
            set
            {
                if (ChainElement.ImageModel.Height == value)
                {
                    return;
                }

                ChainElement.ImageModel = new ImageModel(ChainElement.ImageModel.Width, value, ChainElement.ImageModel.ImageBitDepth);
                OnPropertyChanged();
            }
        }

        public int Port
        {
            get
            {
                return ChainElement.CameraImageProviderTemplate.Port;
            }
            set
            {
                if (ChainElement.CameraImageProviderTemplate.Port == value)
                {
                    return;
                }

                ChainElement.CameraImageProviderTemplate.Port = value;
                OnPropertyChanged();
            }
        }

        public int Timeout
        {
            get
            {
                return ChainElement.CameraImageProviderTemplate.Timeout;
            }
            set
            {
                if (ChainElement.CameraImageProviderTemplate.Timeout == value)
                {
                    return;
                }

                ChainElement.CameraImageProviderTemplate.Timeout = value;
                OnPropertyChanged();
            }
        }

        public string FolderPath
        {
            get
            {
                return ChainElement.FileSystemImageProviderTemplate.FolderPath;
            }
            set
            {
                if (ChainElement.FileSystemImageProviderTemplate.FolderPath == value)
                {
                    return;
                }

                ChainElement.FileSystemImageProviderTemplate.FolderPath = value;
                OnPropertyChanged();
            }
        }

        public string FileSearchPattern
        {
            get
            {
                return ChainElement.FileSystemImageProviderTemplate.FileSearchPattern;
            }
            set
            {
                if (ChainElement.FileSystemImageProviderTemplate.FileSearchPattern == value)
                {
                    return;
                }

                ChainElement.FileSystemImageProviderTemplate.FileSearchPattern = value;
                OnPropertyChanged();
            }
        }

        public IList<Displayable<ImageBitDepth>> AvailableBitDepths { get; }
    }
}