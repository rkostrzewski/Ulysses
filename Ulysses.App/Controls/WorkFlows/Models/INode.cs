using System.Collections.Generic;

namespace Ulysses.App.Controls.WorkFlows.Models
{
    public interface INode
    {
        int NodeId { get; set; }
        int? InputNodeId { get; set; }
        int? OutputNodeId { get; set; }
    }
}
