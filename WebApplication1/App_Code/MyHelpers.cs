using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.App_Code
{
    public static class MyHelpers
    {
        public static MvcHtmlString PrintAuthors(this HtmlHelper html, IEnumerable<AuthorModel> items)
        {
            TagBuilder table = new TagBuilder("table");
            table.AddCssClass("table");
            TagBuilder tr = new TagBuilder("tr");
            TagBuilder th = new TagBuilder("th");
            th.SetInnerText("FirstName");
            tr.InnerHtml += th.ToString();
            th = new TagBuilder("th");
            th.SetInnerText("LastName");
            tr.InnerHtml += th.ToString();
            th = new TagBuilder("th");
            tr.InnerHtml += th.ToString();
            table.InnerHtml += tr.ToString();
            foreach (var item in items)
            {
                tr = new TagBuilder("tr");
                TagBuilder td = new TagBuilder("td");
                td.SetInnerText(item.FirstName);
                tr.InnerHtml += td.ToString();
                td = new TagBuilder("td");
                td.SetInnerText(item.LastName);
                tr.InnerHtml += td.ToString();
                td = new TagBuilder("td");
                string a = "<a class=\"btn btn-warning\" data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#modal-body\" data-target=\"#exampleModalScrollable\" data-toggle=\"modal\" href=\"/Author/EditAndCreate/" + item.Id+ "\">Edit</a> | <a class=\"btn btn-warning\" data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#authors-content\" href=\"/Author/Delete/" + item.Id + "\">Delete</a>";
                td.InnerHtml =a ;
                tr.InnerHtml += td.ToString();
                table.InnerHtml += tr.ToString();
            }
            return new MvcHtmlString(table.ToString());
        }

        public static MvcHtmlString EditAuthors(this HtmlHelper html, AuthorModel item)
        {
            TagBuilder form = new TagBuilder("form");
            form.MergeAttribute("action", "/author/EditAndCreate/"+item.Id.ToString());
            form.MergeAttribute("data-ajax", "true");
            form.MergeAttribute("data-ajax-method", "POST");
            form.MergeAttribute("data-ajax-mode", "replase");
            form.MergeAttribute("data-ajax-update", "#authors-content");
            form.MergeAttribute("method", "post");
            var d = item;
            TagBuilder formBlock = new TagBuilder("div");
            formBlock.AddCssClass("form-horizontal");

            if (item.Id != 0)
            {
                TagBuilder id = new TagBuilder("input");
                id.MergeAttribute("id", "Id");
                id.MergeAttribute("name", "Id");
                id.MergeAttribute("type", "hidden");
                id.MergeAttribute("value", item.Id.ToString());
                formBlock.InnerHtml += id.ToString();
            }

            TagBuilder formGroup = new TagBuilder("div");
            formGroup.AddCssClass("form-group");
            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.MergeAttribute("for", "FirstName");
            label.SetInnerText("FirstName");
            TagBuilder blockLabel = new TagBuilder("div");
            blockLabel.AddCssClass("col-md-10");
            TagBuilder firstName = new TagBuilder("input");
            firstName.AddCssClass("form-control");
            firstName.AddCssClass("text-box");
            firstName.AddCssClass("single-line");
            firstName.MergeAttribute("id", "FirstName");
            firstName.MergeAttribute("name", "FirstName");
            firstName.MergeAttribute("type", "text");
            if(item.FirstName!=null)
            firstName.MergeAttribute("value", item.FirstName.ToString());
            blockLabel.InnerHtml = firstName.ToString();
            formGroup.InnerHtml += label.ToString();
            formGroup.InnerHtml += blockLabel.ToString();
            formBlock.InnerHtml += formGroup.ToString();


            TagBuilder formGroup2 = new TagBuilder("div");
            formGroup2.AddCssClass("form-group");
            label = new TagBuilder("label");
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.MergeAttribute("for", "LastName");
            label.SetInnerText("LastName");
            blockLabel = new TagBuilder("div");
            blockLabel.AddCssClass("col-md-10");
            TagBuilder lastName = new TagBuilder("input");
            lastName.AddCssClass("form-control");
            lastName.AddCssClass("text-box");
            lastName.AddCssClass("single-line");
            lastName.MergeAttribute("id", "LastName");
            lastName.MergeAttribute("name", "LastName");
            lastName.MergeAttribute("type", "text");
            if (item.LastName != null)
                lastName.MergeAttribute("value", item.LastName.ToString());
            blockLabel.InnerHtml = lastName.ToString();
            formGroup2.InnerHtml += label.ToString();
            formGroup2.InnerHtml += blockLabel.ToString();
            formBlock.InnerHtml += formGroup2.ToString();


            TagBuilder formGroup3 = new TagBuilder("div");
            formGroup3.AddCssClass("form-group");
            blockLabel = new TagBuilder("div");
            blockLabel.AddCssClass("col-md-offset-2");
            blockLabel.AddCssClass("col-md-10");

            TagBuilder button1 = new TagBuilder("button");
            button1.AddCssClass("btn");
            button1.AddCssClass("btn-success");
            button1.MergeAttribute("type", "submit");
            button1.MergeAttribute("role", "button");
            button1.SetInnerText("Save");
            blockLabel.InnerHtml += button1.ToString();

            TagBuilder button2 = new TagBuilder("button");
            button2.AddCssClass("btn");
            button2.AddCssClass("btn-secondary");
            button2.MergeAttribute("data-dismiss", "modal");
            button2.MergeAttribute("data-target", "#exampleModalScrollable");
            button2.MergeAttribute("role", "button");
            button2.SetInnerText("Close");
            blockLabel.InnerHtml += button2.ToString();

            formGroup3.InnerHtml += blockLabel.ToString();
            formBlock.InnerHtml += formGroup3.ToString();


            form.InnerHtml = formBlock.ToString();
            return new MvcHtmlString(form.ToString());
        }
    }
}