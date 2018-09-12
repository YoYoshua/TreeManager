using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeManager.Domain.Abstract;
using TreeManager.Domain.Entities;

namespace TreeManager.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private INodeRepository repository;

        public AdminController(INodeRepository paramRepo)
        {
            this.repository = paramRepo;
        }
        public ActionResult AddNode()
        {
            return View("Add");
        }

        [HttpPost]
        public ActionResult AddNode(int? id, Node formData)
        {
            if(ModelState.IsValid)
            {
                if(id.HasValue)
                {
                    formData.Parent = repository.GetNodeByID((int)id);

                    if (formData.Parent.ChildNodes == null)
                        formData.Parent.ChildNodes = new List<Node>();

                    formData.Parent.ChildNodes.Add(formData);
                }
                repository.AddNode(formData);

                return RedirectToAction("Tree", "Tree");
            }

            return View("Add", formData);
        }

        public ActionResult EditNode(int id)
        {
            Node tempNode = repository.GetNodeByID(id);

            return View("Edit", tempNode);
        }

        [HttpPost]
        public ActionResult EditNode(int id, Node formData)
        {
            if (ModelState.IsValid)
            {
                Node targetNode = repository.GetNodeByID(id);

                targetNode.Title = formData.Title;
                targetNode.Description = formData.Description;

                repository.UpdateNode(targetNode);

                return RedirectToAction("Tree", "Tree");
            }

            return View("Edit", formData);
        }

        public ActionResult DeleteNode(int id)
        {
            Node targetNode = repository.GetNodeByID(id);
            try
            {
                repository.DeleteNode(targetNode);
            }
            catch (ArgumentNullException)
            {
                ViewBag.Error = "Wystąpił błąd.";
            }

            return RedirectToAction("Tree", "Tree");
        }
    }
}