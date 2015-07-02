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

using Umbraco.Core.Models;

namespace Umbraco.Pylon
{
    /// <summary>
    /// A base view model class for view models wrapping a single document type content item.
    /// </summary>
    public abstract class LinkedViewModelBase<TInnerObject>
        where TInnerObject : class, IDocumentType
    {
        private TInnerObject _innerObject;

        /// <summary>
        /// Gets or sets the inner object.
        /// </summary>
        /// <value>
        /// The inner object.
        /// </value>
        /// <remarks>
        /// Changing the value of the InnerObject will trigger the InitModel() method.
        /// </remarks>
        protected TInnerObject InnerObject
        {
            get { return _innerObject; }
            set
            {
                _innerObject = value;
                InitModel();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the model has content.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the model has content; otherwise, <c>false</c>.
        /// </value>
        public bool HasContent { get { return InnerObject != null && InnerObject.HasContent; } }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get { return InnerObject.Name; } }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get { return InnerObject.Url; } }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public IPublishedContent Content { get { return InnerObject.Content; } }

        /// <summary>
        /// Initializes the model.
        /// </summary>
        protected abstract void InitModel();
    }
}