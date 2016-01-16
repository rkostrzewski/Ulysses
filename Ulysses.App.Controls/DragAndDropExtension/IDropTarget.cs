namespace Ulysses.App.Controls.DragAndDropExtension
{
    public interface IDropTarget
    {
        void DragOver(DropInfo dropInfo);

        void Drop(DropInfo dropInfo);
    }
}