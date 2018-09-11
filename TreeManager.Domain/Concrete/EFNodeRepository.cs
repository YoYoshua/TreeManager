using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManager.Domain.Abstract;
using TreeManager.Domain.Entities;

namespace TreeManager.Domain.Concrete
{
    public class EFNodeRepository : INodeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Node> Nodes
        {
            get { return context.Nodes; }
        }

        public IEnumerable<Node> GetChildNodes(Node paramNode)
        {
            return context.Nodes.Where(m => m.Parent.NodeID == paramNode.NodeID);
        }
    }
}
