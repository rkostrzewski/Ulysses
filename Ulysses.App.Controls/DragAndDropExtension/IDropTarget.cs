namespace Ulysses.App.Controls.DragAndDropExtension
{
    public interface IDropTarget
    {
        void DragOver(IDropInfo dropInfo);

        void Drop(IDropInfo dropInfo);
    }
}