using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeManager.Domain.Abstract;
using TreeManager.Domain.Entities;
using TreeManager.WebUI.HtmlHelpers;

namespace TreeManager.WebUI.Controllers
{
    public class TreeController : Controller
    {
        //przygotowanie listy zawierajacej elementy do wyswietlenia
        private static List<int> NodeIDsToExpand = new List<int>();

        private INodeRepository repository;

        public TreeController(INodeRepository paramRepo)
        {
            this.repository = paramRepo;
        }

        public ViewResult Tree()
        {
            //slownik zawierajacy informacje o rozwinieciu i zwinieciu wezla
            Dictionary<Node, bool> expand = new Dictionary<Node, bool>();

            //skojarzenie ze soba wartosci bool i List w celu wyswietlenia
            foreach(var n in repository.Nodes)
            {
                if (NodeIDsToExpand.Any(m => m == n.NodeID))
                    expand.Add(n, true);
                else
                    expand.Add(n, false);

            }

            //dziwne - bez tego nie wyświetla poprawnie
            foreach (var n in repository.Nodes)
            {
                if (n.ChildNodes != null)
                {
                    Debug.WriteLine(n.ChildNodes.ToString());
                }
            }

            return View(expand);
        }

        public ActionResult ExpandNode(int id)
        {
            if (NodeIDsToExpand.Any(m => m == id))
                return RedirectToAction("Tree");
            else
            {
                NodeIDsToExpand.Add(id);
                return RedirectToAction("Tree");
            }
        }

        public ActionResult CollapseNode(int id)
        {
            if (NodeIDsToExpand.Any(m => m == id))
            {
                NodeIDsToExpand.Remove(id);
                return RedirectToAction("Tree");
            }
            else
            {
                return RedirectToAction("Tree");
            }
        }

        public ViewResult Add()
        {
            repository.AddNode(new Node
            {
                Title = "TestDodajWęzeł",
                Description = "Testowy węzeł dodany",
                Parent = null,
                ChildNodes = null
            });
            return View("Tree", repository.Nodes);
        }
    }
}