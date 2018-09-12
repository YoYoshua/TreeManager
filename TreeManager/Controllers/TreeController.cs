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
        private INodeRepository repository;

        public TreeController(INodeRepository paramRepo)
        {
            this.repository = paramRepo;
        }

        public ViewResult Tree()
        {
            //dziwne - bez tego nie wyświetla poprawnie
            foreach (var n in repository.Nodes)
            {
                if (n.ChildNodes != null)
                {
                    Debug.WriteLine(n.ChildNodes.ToString());
                }
            }
            return View(repository.Nodes);
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