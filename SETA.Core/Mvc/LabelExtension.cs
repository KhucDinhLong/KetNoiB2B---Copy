﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using System.Web.Routing;

public static class LabelExtension
{
    public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, string extraText = "", string replaceLabel = "")
    {
        return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes), extraText, replaceLabel);
    }
    public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string extraText = "", string replaceLabel = "")
    {
        ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
        string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
        string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
        if (String.IsNullOrEmpty(labelText))
        {
            return MvcHtmlString.Empty;
        }

        var tag = new TagBuilder("label");
        tag.MergeAttributes(htmlAttributes);
        tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
        tag.SetInnerText(labelText);
        return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
    }
}