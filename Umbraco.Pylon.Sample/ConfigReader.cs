using System;
using System.Web.Configuration;

namespace Umbraco.Pylon.Sample
{
    /// <summary>
    /// A pass through reader for configuration information.
    /// </summary>
    public class ConfigReader : IConfigReader
    {
        public string SomeValue
        {
            get { return WebConfigurationManager.AppSettings["SomeValue"] ?? String.Empty; }
        }
    }
}