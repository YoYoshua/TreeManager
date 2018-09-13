using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeManager.Domain.Abstract;
using TreeManager.Domain.Concrete;
using TreeManager.Domain.Entities;

namespace TreeManager.WebUI.Controllers
{
    public class NodeAdminController : Controller
    {
        private INodeRepository repository;

        public NodeAdminController(INodeRepository paramRepo)
        {
            this.repository = paramRepo;
        }

        [AccessDeniedAuthorize(Roles = "Admin")]
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

        [AccessDeniedAuthorize(Roles = "Admin")]
        public ActionResult EditNode(int id)
        {
            Node tempNode = repository.GetNodeByID(id);

            return View("Edit", tempNode);
        }

        [HttpPost]
        [AccessDeniedAuthorize(Roles = "Admin")]
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

        [AccessDeniedAuthorize(Roles = "Admin")]
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

        [HttpPost]
        [AccessDeniedAuthorize(Roles = "Admin")]
        public void SwapNode(string o)
        {
            var saveObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SwapNodeModel>(o);
            repository.SwapNode(repository.GetNodeByID(saveObject.Element1), 
                repository.GetNodeByID(saveObject.Element2));
        }

        private class SwapNodeModel
        {
            public int Element1 { get; set; }
            public int Element2 { get; set; }
        }
    }
}