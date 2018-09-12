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
            get { return context.Nodes.Include("ChildNodes").Include("Parent"); }
        }

        public Node GetNodeByID(int id)
        {
            return context.Nodes.FirstOrDefault(n => n.NodeID == id);
        }

        public void AddNode(Node paramNode)
        {
            if(paramNode == null)
            {
                throw new ArgumentNullException();
            }

            context.Nodes.Add(paramNode);
            context.SaveChanges();
        }

        public void AddNode(Node paramNode, Node parentNode)
        {
            if (paramNode == null)
            {
                throw new ArgumentNullException();
            }

            if (parentNode == null)
            {
                AddNode(paramNode);
            }
            else
            {
                context.Nodes.Attach(parentNode);
                context.Nodes.Add(paramNode);
                context.SaveChanges();
            }
        }

        public void UpdateNode(Node paramNode)
        {
            if (paramNode == null)
            {
                throw new ArgumentNullException();
            }
            context.SaveChanges();
        }

        public void DeleteNode(Node paramNode)
        {
            if (paramNode == null)
            {
                throw new ArgumentNullException();
            }

            List<Node> Children = GetChildNodes(paramNode).ToList<Node>();

            if(Children != null)
            {
                foreach(var c in Children)
                {
                    DeleteNode(c);
                }
            }

            context.Nodes.Attach(paramNode);
            context.Nodes.Remove(paramNode);
            context.SaveChanges();
        }

        public IEnumerable<Node> GetChildNodes(Node paramNode)
        {
            return context.Nodes.Where(m => m.Parent.NodeID == paramNode.NodeID);
        }
    }
}
