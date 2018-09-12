using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TreeManager.Domain.Abstract;
using TreeManager.Domain.Entities;

namespace TreeManager.WebUI.HtmlHelpers
{
    public static class TreeHelper
    {
        public static MvcHtmlString GenerateTree(this HtmlHelper htmlHelper, IEnumerable<Node> nodes)
        {
            //zbuduj korzenie
            StringBuilder result = new StringBuilder();
            foreach(var n in nodes)
            {
                if(n.Parent == null)
                {
                    result.Append(RecursiveTreeGenerator(n));
                }
            }
            result.Append(GenerateEmptyNode());
            return MvcHtmlString.Create(result.ToString());
        }

        //rekursywna metoda generująca poddrzewa dla podanego węzła
        private static String RecursiveTreeGenerator(Node currentNode)
        {
            StringBuilder result = new StringBuilder();

            //zbuduj węzeł
            TagBuilder tag_li = new TagBuilder("li");
            TagBuilder tag_ul = new TagBuilder("ul");
            TagBuilder button = new TagBuilder("button");

            button.SetInnerText("-");
            result.Append(currentNode.Title + button.ToString());
            result = WrapIntoTag(result, tag_li);

            //rekursywnie zbuduj dzieci
            if(currentNode.ChildNodes != null)
            {
                StringBuilder childResult = new StringBuilder();
                foreach(var c in currentNode.ChildNodes)
                {
                    childResult.Append(RecursiveTreeGenerator(c));
                }
                childResult.Append(GenerateEmptyNode());
                childResult = WrapIntoTag(childResult, tag_ul);
                result.Append(childResult.ToString());
            }
            return result.ToString();
        }

        private static StringBuilder WrapIntoTag (StringBuilder target, TagBuilder tag)
        {
            tag.InnerHtml = target.ToString();
            target.Clear();
            target.Append(tag.ToString());
            return target;
        }

        private static String GenerateEmptyNode ()
        {
            TagBuilder tag_li = new TagBuilder("li");
            TagBuilder button = new TagBuilder("button");
            button.SetInnerText("+");
            tag_li.InnerHtml = button.ToString();
            return tag_li.ToString();
        }
    }
}