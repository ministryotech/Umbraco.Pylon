using Umbraco.Core.Models;

namespace Umbraco.Pylon.TestSupport.Concretes
{
    /// <summary>
    /// A representation of a DocumentType
    /// </summary>
    public class ConcreteDocumentType : DocumentType
    {
        #region | Construction |

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteDocumentType" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public ConcreteDocumentType(dynamic content)
        {
            DynamicContent = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteDocumentType" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public ConcreteDocumentType(IPublishedContent content)
            : base(content)
        {
            BodyText = Get.StringValue("BodyText");
            NumericValue = Get.NumericValue("Number");
        }

        #endregion

        public string BodyText { get; set; }
        public int NumericValue { get; set; }
    }
}
