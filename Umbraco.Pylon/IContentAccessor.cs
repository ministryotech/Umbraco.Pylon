// Copyright (c) 2016 Minotech Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files
// (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Umbraco.Pylon
{
    /// <summary>
    /// Accesses content elements
    /// </summary>
    public interface IContentAccessor
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        IPublishedContent ContentObject { get; set; }

        /// <summary>
        /// Gets or sets the get content function.
        /// </summary>
        /// <value>
        /// The get content function.
        /// </value>
        Func<int, IPublishedContent> GetContentFunc { set; }

        /// <summary>
        /// Sets up the ContentObject with content for a specific ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IPublishedContent Content(int id);

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        object Value(string propertyName);

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="contentObject">The content object.</param>
        /// <returns></returns>
        object Value(IPublishedContent contentObject, string propertyName);

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        string StringValue(string propertyName);

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="contentObject">The content object.</param>
        /// <returns></returns>
        string StringValue(IPublishedContent contentObject, string propertyName);

        /// <summary>
        /// Gets a collection from delimited string property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        IEnumerable<string> DelimitedStringCollection(string propertyName, char delimiter = ',');

        /// <summary>
        /// Gets a collection from delimited string property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="contentObject">The content object.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        IEnumerable<string> DelimitedStringCollection(IPublishedContent contentObject, string propertyName, char delimiter = ',');

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        int NumericValue(string propertyName);

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        int NumericValue(IPublishedContent contentObject, string propertyName);

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        int? NullableNumericValue(string propertyName);

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        int? NullableNumericValue(IPublishedContent contentObject, string propertyName);

        /// <summary>
        /// Gets the content property in boolean form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        bool BooleanValue(string propertyName);

        /// <summary>
        /// Gets the content property in boolean form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        bool BooleanValue(IPublishedContent contentObject, string propertyName);

        /// <summary>
        /// Gets the content property in date form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultDateTime">The default date time.</param>
        /// <returns></returns>
        DateTime DateValue(string propertyName, DateTime defaultDateTime);

        /// <summary>
        /// Gets the content property in date form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultDateTime">The default date time.</param>
        /// <returns></returns>
        DateTime DateValue(IPublishedContent contentObject, string propertyName, DateTime defaultDateTime);
    }
}