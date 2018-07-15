﻿//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.10.102
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;
using Umbraco.Web;

namespace EmptyUmbracoWebApp.Models.Content
{
    /// <summary>Home</summary>
    [PublishedContentModel("Home")]
    public partial class Home : PublishedContentModel
    {
#pragma warning disable 0109 // new is redundant
        public new const string ModelTypeAlias = "Home";
        public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

        public Home(IPublishedContent content)
            : base(content)
        { }

#pragma warning disable 0109 // new is redundant
        public new static PublishedContentType GetModelContentType()
        {
            return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
        }
#pragma warning restore 0109

        public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Home, TValue>> selector)
        {
            return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
        }

        ///<summary>
        /// Nav Text: The text to display in the navigation.
        ///</summary>
        [ImplementPropertyType("navText")]
        public string NavText
        {
            get { return this.GetPropertyValue<string>("navText"); }
        }

        ///<summary>
        /// Primary Header: The header to show on the page.
        ///</summary>
        [ImplementPropertyType("primaryHeader")]
        public string PrimaryHeader
        {
            get { return this.GetPropertyValue<string>("primaryHeader"); }
        }

        ///<summary>
        /// Body Text: The opening text for the home page.
        ///</summary>
        [ImplementPropertyType("bodyText")]
        public IHtmlString BodyText
        {
            get { return this.GetPropertyValue<IHtmlString>("bodyText"); }
        }
    }
}
