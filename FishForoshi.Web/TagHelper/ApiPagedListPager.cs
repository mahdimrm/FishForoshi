using FishForoshi.Abstraction.Tools;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Presentation.MvcUI.Infrastructure.TagHelper
{
    public static class ApiPagedListPagerHelpers
    {
        public static IHtmlContent PagedListPager<T>(this IHtmlHelper htmlHelper, IPagedList<T> Model, Func<int, string> generatePageUrl)
        {

            var Main = new TagBuilder("div");
            Main.AddCssClass("pagination-container");
            Main.AddCssClass("text-center");
            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");
            if (Model.HasPreviousPage)
            {
                SetLink(ulTag, "قبلی", generatePageUrl(Model.PageNumber - 1), false);
                if (Model.PageNumber > 5)
                {
                    SetLink(ulTag, "اول", generatePageUrl(1), false);
                }
            }
            var nPages = Getpages(Model);
            for (int i = nPages.Item1; i < nPages.Item2; i++)
            {
                SetLink(ulTag, i.ToString(), generatePageUrl(i), Model.PageNumber == i);
            }

            if (Model.HasNextPage)
            {
                if (Model.PageNumber < Model.TotalPages - 4)
                {
                    SetLink(ulTag, "آخر", generatePageUrl(Model.TotalPages), false);
                }
                SetLink(ulTag, "بعدی", generatePageUrl(Model.PageNumber + 1), false);
            }
            Main.InnerHtml.AppendHtml(ulTag.RenderStartTag());
            Main.InnerHtml.AppendHtml(ulTag.RenderBody());
            Main.InnerHtml.AppendHtml(ulTag.RenderEndTag());

            return Main.RenderBody();
        }

        public static void SetLink(TagBuilder tagBuilder, string text, string href, bool active = false)
        {
            var liTag = new TagBuilder("li");

            var aTag = new TagBuilder("a");
            if (active)
            {
                liTag.AddCssClass("paginate_button page-item active");
            }
            aTag.MergeAttribute("href", href);
            aTag.AddCssClass("page-link");
            aTag.InnerHtml.AppendHtml(text);
            liTag.InnerHtml.AppendHtml(aTag.RenderStartTag());
            liTag.InnerHtml.AppendHtml(aTag.RenderBody());
            liTag.InnerHtml.AppendHtml(aTag.RenderEndTag());
            tagBuilder.InnerHtml.AppendHtml(liTag.RenderStartTag());
            tagBuilder.InnerHtml.AppendHtml(liTag.RenderBody());
            tagBuilder.InnerHtml.AppendHtml(liTag.RenderEndTag());

        }

        public static Tuple<int, int> Getpages<T>(IPagedList<T> Model)
        {
            if (Model.TotalPages <= 10)
            {
                return new Tuple<int, int>(1, Model.TotalPages + 1);
            }
            else if (Model.PageNumber <= 5)
            {
                return new Tuple<int, int>(1, 10);
            }
            else if (Model.PageNumber > Model.TotalPages - 5)
            {
                return new Tuple<int, int>(Model.TotalPages - 8, Model.TotalPages + 1);
            }
            return new Tuple<int, int>(Model.PageNumber - 4, Model.PageNumber + 5);
        }
    }

}
