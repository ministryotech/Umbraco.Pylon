using System;
using System.Runtime.Serialization;

namespace Umbraco.Pylon
{
    /// <summary>
    /// A Pylon application invalid operation error.
    /// </summary>
    /// <seealso cref="InvalidOperationException" />
    public class PylonException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PylonException"/> class.
        /// </summary>
        public PylonException()
            : this ("This operation is invalid within a Pylon managed Umbraco site.")
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PylonException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PylonException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public PylonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
