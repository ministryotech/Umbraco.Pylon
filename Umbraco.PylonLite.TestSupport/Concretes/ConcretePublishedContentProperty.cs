using System;
using Umbraco.Core.Models;

namespace Umbraco.PylonLite.TestSupport.Concretes
{
    /// <summary>
    /// Concrete property implementation to aid testing.
    /// </summary>
    public class ConcretePublishedContentProperty : IPublishedProperty
    {
        public string Alias { get; set; }
        public object Value { get; set; }
        public Guid Version { get; set; }
        public object DataValue { get; set; }
        public bool HasValue { get; set; }
        public string PropertyTypeAlias { get; set; }
        public object XPathValue { get; set; }
    }
}
