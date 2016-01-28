namespace Ulysses.ImageProviders.Templates
{
    public class FileSystemImageProviderTemplate
    {
        public string FolderPath { get; set; }
        public string FileSearchPattern { get; set; }
        public bool InfiniteLoop { get; set; }
    }
}