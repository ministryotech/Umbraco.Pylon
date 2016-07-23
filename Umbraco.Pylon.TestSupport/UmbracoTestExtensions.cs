using System.Collections.Generic;
using Moq;
using Umbraco.Core.Models;

namespace Umbraco.PylonLite.TestSupport
{
    /// <summary>
    /// Extension menthods to aid testing Umbraco data and simple mock builders.
    /// </summary>
    public static class UmbracoTestExtensions
    {
        /// <summary>
        /// Gets a Mocked Published Property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Mock<IPublishedProperty> PublishedProperty(string propertyName, object value)
        {
            var mockProperty = new Mock<IPublishedProperty>();
            mockProperty.SetupAllProperties();
            mockProperty.SetupGet(p => p.HasValue).Returns(value != null);
            mockProperty.SetupGet(p => p.PropertyTypeAlias).Returns(propertyName);
            mockProperty.SetupGet(p => p.Value).Returns(value);
            mockProperty.SetupGet(p => p.DataValue).Returns(value);

            return mockProperty;
        }

        /// <summary>
        /// Sets up an Umbraco property.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public static void SetupContentProperty(this Mock<IPublishedContent> content, string propertyName, object value)
        {
            var newProperty = PublishedProperty(propertyName, value).Object;

            content.SetupAllProperties();
            content.Setup(c => c.GetProperty(propertyName)).Returns(newProperty);
            content.Setup(c => c.GetProperty(propertyName, It.IsAny<bool>())).Returns(newProperty);

            if (content.Object.Properties == null)
            {
                content.Setup(c => c.Properties).Returns(new List<IPublishedProperty>
                {
                    newProperty
                });
            }
            else
            {
                content.Object.Properties.Add(newProperty);
            }
        }

        /// <summary>
        /// Verifies the content properties are called.
        /// </summary>
        /// <param name="mockContent">The mockContent object to verify.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="times">The times variable.</param>
        public static void VerifyContentPropertiesCalled(this Mock<IPublishedContent> mockContent,
            IEnumerable<string> propertyNames, Times times)
        {
            foreach (var name in propertyNames)
            {
                var innerName = name;
                mockContent.Verify(content => content.GetProperty(innerName), times, name + " not invoked.");
            }
        }

        /// <summary>
        /// Verifies the content properties are called.
        /// </summary>
        /// <param name="mockContent">The mockContent object to verify.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="childPropertyNames">The child property names.</param>
        /// <param name="times">The times variable.</param>
        public static void VerifyContentPropertiesCalled(this Mock<IPublishedContent> mockContent,
            IEnumerable<string> propertyNames, IEnumerable<string> childPropertyNames, Times times)
        {
            VerifyContentPropertiesCalled(mockContent, propertyNames, times);

            foreach (var name in childPropertyNames)
            {
                var innerName = name;
                mockContent.Verify(content => content.GetProperty(innerName, It.IsAny<bool>()), times,
                    name + " not invoked.");
            }
        }
    }
}
