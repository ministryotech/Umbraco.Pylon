// Copyright (c) 2015 Minotech Ltd.
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

namespace Umbraco.PylonLite
{
    /// <summary>
    /// Accesses content elements
    /// </summary>
    public class ContentAccessor : IContentAccessor
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        public IPublishedContent ContentObject { get; set; }

        /// <summary>
        /// Sets up the ContentObject with content for a specific ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IPublishedContent Content(int id)
        {
            ContentObject = GetContentFunc(id);
            return ContentObject;
        }

        /// <summary>
        /// Gets or sets the get content function.
        /// </summary>
        /// <value>
        /// The get content function.
        /// </value>
        public Func<int, IPublishedContent> GetContentFunc { private get; set; }

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public object Value(string propertyName)
        {
            if (ContentObject == null) return null;

            var objectContent = ContentObject.GetProperty(propertyName);
            return objectContent == null ? null : objectContent.Value;
        }

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="contentObject">The content object.</param>
        /// <returns></returns>
        public object Value(IPublishedContent contentObject, string propertyName)
        {
            ContentObject = contentObject;
            return Value(propertyName);
        }

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public string StringValue(string propertyName)
        {
            if (ContentObject == null) return null;

            var objectContent = ContentObject.GetProperty(propertyName);
            if (objectContent == null) return null;

            return objectContent.Value == null ? null : objectContent.Value.ToString();
        }

        /// <summary>
        /// Gets a content property from content.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="contentObject">The content object.</param>
        /// <returns></returns>
        public string StringValue(IPublishedContent contentObject, string propertyName)
        {
            ContentObject = contentObject;
            return StringValue(propertyName);
        } 

        /// <summary>
        /// Gets a collection from delimited string property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public IEnumerable<string> DelimitedStringCollection(string propertyName, char delimiter = ',')
        {
            var obj = StringValue(propertyName);
            return !String.IsNullOrWhiteSpace(obj)
                             ? obj.Split(delimiter)
                             : new string[0];
        }

        /// <summary>
        /// Gets a collection from delimited string property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="contentObject">The content object.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public IEnumerable<string> DelimitedStringCollection(IPublishedContent contentObject, string propertyName, char delimiter = ',')
        {
            ContentObject = contentObject;
            return DelimitedStringCollection(propertyName, delimiter);
        }

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public int NumericValue(string propertyName)
        {
            var potentialContent = StringValue(propertyName);
            int value;
            return (potentialContent != null && int.TryParse(potentialContent, out value)) ? value : 0;
        }

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public int NumericValue(IPublishedContent contentObject, string propertyName)
        {
            ContentObject = contentObject;
            return NumericValue(propertyName);
        }

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public int? NullableNumericValue(string propertyName)
        {
            var potentialContent = StringValue(propertyName);

            int value;
            return (potentialContent != null)
                    ? Int32.TryParse(potentialContent, out value) ? (int?)value : null
                    : null;
        }

        /// <summary>
        /// Gets the content property in numeric form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public int? NullableNumericValue(IPublishedContent contentObject, string propertyName)
        {
            ContentObject = contentObject;
            return NullableNumericValue(propertyName);
        }

        /// <summary>
        /// Gets the content property in boolean form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public bool BooleanValue(string propertyName)
        {
            var potentialContent = StringValue(propertyName);
            bool value;
            return (potentialContent != null && bool.TryParse(potentialContent, out value)) && value;
        }

        /// <summary>
        /// Gets the content property in boolean form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public bool BooleanValue(IPublishedContent contentObject, string propertyName)
        {
            ContentObject = contentObject;
            return BooleanValue(propertyName);
        }

        /// <summary>
        /// Gets the content property in date form
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultDateTime">The default date time.</param>
        /// <returns></returns>
        public DateTime DateValue(string propertyName, DateTime defaultDateTime)
        {
            var potentialContent = StringValue(propertyName);
            DateTime value;
            return (potentialContent != null && DateTime.TryParse(potentialContent, out value)) ? value : defaultDateTime;
        }

        /// <summary>
        /// Gets the content property in date form
        /// </summary>
        /// <param name="contentObject">The content object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultDateTime">The default date time.</param>
        /// <returns></returns>
        public DateTime DateValue(IPublishedContent contentObject, string propertyName, DateTime defaultDateTime)
        {
            ContentObject = contentObject;
            return DateValue(propertyName, defaultDateTime);
        }
    }
}