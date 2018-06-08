using System;
using NUnit.Framework;
using Moq;
using Umbraco.Core.Models;
using Umbraco.Core.Dynamics;
using Umbraco.Pylon.TestSupport;
using Umbraco.Pylon.TestSupport.Concretes;

namespace Umbraco.Pylon.Tests
{
    [TestFixture]
    [Category("Model Test")]
    public class DocumentTypeTests
    {
        private ConcreteDocumentType objUt;

        private Mock<IPublishedContent> mockContent;

        #region | Setup and TearDown |

        [SetUp]
        public void SetUp()
        {
            mockContent = new Mock<IPublishedContent>();
        }

        [TearDown]
        public void TearDown()
        {
            mockContent = null;
            objUt = null;
        }

        #endregion

        [Test]
        public void CanLoadStringsFromContent()
        {
            var bodyTextContent = new ConcretePublishedContentProperty { Value = TestConstants.BodyTextTestString };
            mockContent.Setup(content => content.GetProperty("BodyText")).Returns(bodyTextContent);

            objUt = new ConcreteDocumentType(mockContent.Object);

            Assert.That(objUt.BodyText == bodyTextContent.Value.ToString());
        }

        [Test]
        public void CanLoadNumericsFromContent()
        {
            var numericContent = new ConcretePublishedContentProperty { Value = 15 };
            mockContent.Setup(content => content.GetProperty("Number")).Returns(numericContent);

            objUt = new ConcreteDocumentType(mockContent.Object);

            Assert.That(objUt.NumericValue == Convert.ToInt32(numericContent.Value));
        }

        [Test]
        public void WillNotGetAPropertyThatDoesNotExist()
        {
            mockContent.Setup(content => content.GetProperty("BodyText")).Returns((IPublishedProperty)null);

            objUt = new ConcreteDocumentType(mockContent.Object);

            Assert.That(objUt.BodyText == null);
        }

        [Test]
        public void WillTellTheConsumerWhenContentIsPresent()
        {
            mockContent.SetupGet(content => content.Id).Returns(12);

            objUt = new ConcreteDocumentType(mockContent.Object);

            Assert.That(objUt.HasContent);
        }

        [Test]
        public void WillReturnVariousCommonContentElementsFromTheUnderlyingUmbracoLayer()
        {
            mockContent.SetupGet(content => content.Id).Returns(12);
            mockContent.SetupGet(content => content.Name).Returns(TestConstants.NameString);

            objUt = new ConcreteDocumentType(mockContent.Object);

            Assert.That(objUt.Id == 12);
            Assert.That(objUt.Name == TestConstants.NameString);
        }

        [Test]
        public void WillTellTheConsumerWhenContentIsNotPresentById()
        {
            mockContent.SetupGet(content => content.Id).Returns(0);

            objUt = new ConcreteDocumentType(mockContent.Object);

            Assert.That(!objUt.HasContent);
        }

        [Test]
        public void WillTellTheConsumerWhenContentIsNotPresentByNull()
        {
            objUt = new ConcreteDocumentType(DynamicNull.Null);

            Assert.That(!objUt.HasContent);
        }

        [Test]
        public void TheContentPropertyWillReturnNullIfTheDynamicContentValueIsNull()
        {
            objUt = new ConcreteDocumentType(DynamicNull.Null);

            Assert.That(objUt.Content == null);
        }
    }
}
