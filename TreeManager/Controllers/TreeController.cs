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
        private static List<int> NodeIDsToSort = new List<int>();
        private static bool SortRoot = false;

        private INodeRepository repository;

        public TreeController(INodeRepository paramRepo)
        {
            this.repository = paramRepo;
        }

        public ViewResult Tree()
        {
            //slownik zawierajacy informacje o rozwinieciu i zwinieciu wezla
            Dictionary<Node, bool> expandDict = new Dictionary<Node, bool>();
            Dictionary<Node, bool> sortDict = new Dictionary<Node, bool>();

            //skojarzenie ze soba wartosci bool i List w celu wyswietlenia
            //druga czesc sortuje badz nie w zaleznosci od wlasciwosci parametru Sort
            foreach(var n in SortRoot ? repository.Nodes.OrderBy(m => m.Title) : repository.Nodes)
            {
                if (NodeIDsToExpand.Any(m => m == n.NodeID))
                    expandDict.Add(n, true);
                else
                    expandDict.Add(n, false);
            }

            //jw tylko sortowanie dla wezlow
            foreach(var n in repository.Nodes)
            {
                if (NodeIDsToSort.Any(m => m == n.NodeID))
                    sortDict.Add(n, true);
                else
                    sortDict.Add(n, false);
            }

            //zapisz wartosc parametru sort w zmiennej sesyjnej (do wyswietlania)
            Session["Sort"] = SortRoot;
            Session["SortDict"] = sortDict;

            return View(expandDict);
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

        public ActionResult SortNodes()
        {
            SortRoot = !SortRoot;
            return RedirectToAction("Tree");
        }

        public ActionResult SortNode(int id)
        {
            if (NodeIDsToSort.Any(m => m == id))
                NodeIDsToSort.Remove(id);
            else
                NodeIDsToSort.Add(id);

            return RedirectToAction("Tree");
        }
    }
}