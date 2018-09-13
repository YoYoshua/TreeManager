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

        public void SwapNode(Node node1, Node node2)
        {
            if(node1 == null || node2 == null)
            {
                throw new ArgumentNullException();
            }

            //przygotuj chwilowy wezel oraz liste dzieci pierwszego i drugiego wezla
            Node tempNode = new Node();
            List<Node> children1 = GetChildNodes(node1).ToList<Node>();
            List<Node> children2 = GetChildNodes(node2).ToList<Node>();

            //przepisz dane wezla pierwszego do wezla tymczasowego
            tempNode.Title = node1.Title;
            tempNode.Description = node1.Description;

            //przepisz dane z wezla drugiego do pierwszego
            node1.Title = node2.Title;
            node1.Description = node2.Description;

            //przepisz dane z wezla tymczasowego do drugiego
            node2.Title = tempNode.Title;
            node2.Description = tempNode.Description;

            //zapisz zmiany
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
