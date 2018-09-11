using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManager.Domain.Entities;

namespace TreeManager.Domain.Abstract
{
    public interface INodeRepository
    {
        IEnumerable<Node> Nodes { get; }
    }
}
