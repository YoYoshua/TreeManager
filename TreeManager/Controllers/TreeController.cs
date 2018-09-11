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
            HtmlHelper myHelper = null;
            Debug.WriteLine(myHelper.GenerateTree(repository.Nodes).ToString());
            return View(repository.Nodes);
        }
    }
}