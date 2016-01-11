using System.Windows;

namespace Ulysses.App.Controls.Workflows.Connections
{
    internal struct ConnectorInfo
    {
        public double ItemLeft { get; set; }
        public double ItemTop { get; set; }
        public Size ItemSize { get; set; }
        public Point Position { get; set; }
        public ConnectorOrientation Orientation { get; set; }
    }
}