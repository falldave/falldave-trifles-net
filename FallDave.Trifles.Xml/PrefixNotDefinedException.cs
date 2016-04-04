using System;
using System.Runtime.Serialization;
using System.Xml;

namespace FallDave.Trifles.Xml
{
    /// <summary>
    /// Represents a failure of the program to locate a given XML namespace prefix within some name-resolving context.
    /// </summary>
    [Serializable]
    public class PrefixNotDefinedException : XmlException
    {
        // These could be converted into resources.
        private static readonly string DefaultMessage = "The specified prefix is not defined by this resolver.";

        private static readonly string PrefixClauseFormat = "Prefix: {0}";

        // Keys for serialization
        private static readonly string PrefixInfoKey = "Prefix";

        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the PrefixNotDefinedException class with the default error message.
        /// </summary>
        public PrefixNotDefinedException() : base(DefaultMessage)
        { }

        /// <summary>
        /// Initializes a new instance of the PrefixNotDefinedException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. If <c>null</c>, the default error message is used.</param>
        public PrefixNotDefinedException(string message) : base(message ?? DefaultMessage)
        { }

        /// <summary>
        /// Initializes a new instance of the PrefixNotDefinedException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. If <c>null</c>, the default error message is used.</param>
        /// <param name="innerException">The exception that is the cause of the current exception (<c>null</c> indicates unspecified).</param>
        public PrefixNotDefinedException(string message, Exception innerException) : base(message ?? DefaultMessage, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the PrefixNotDefinedException class with a specified error message and prefix.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. If <c>null</c>, the default error message is used.</param>
        /// <param name="prefix">The name of the prefix that could not be found (<c>null</c> indicates unspecified).</param>
        public PrefixNotDefinedException(string message, string prefix) : base(message ?? DefaultMessage)
        {
            this.prefix = prefix;
        }

        /// <summary>
        /// Initializes a new instance of the PrefixNotDefinedException class with a specified error message and prefix and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. If <c>null</c>, the default error message is used.</param>
        /// <param name="prefix">The name of the prefix that could not be found (<c>null</c> indicates unspecified).</param>
        /// <param name="innerException">The exception that is the cause of the current exception (<c>null</c> indicates unspecified).</param>
        public PrefixNotDefinedException(string message, string prefix, Exception innerException) : base(message, innerException)
        {
            this.prefix = prefix;
        }

        /// <summary>
        /// Initializes a new instance of the PrefixNotDefinedException class with serialized data.
        /// </summary>
        /// <param name="info">A store containing information to be deserialized to a new instance.</param>
        /// <param name="context">Data describing the stream used to transfer serialized information.</param>
        protected PrefixNotDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.prefix = info.GetString(PrefixInfoKey);
        }

        /// <summary>
        /// A message describing this exception.
        /// </summary>
        public override string Message
        {
            get
            {
                var message = base.Message;
                var prefixClause = string.IsNullOrEmpty(prefix) ? "" : GetPrefixClause(prefix);
                return message + prefixClause;
            }
        }

        /// <summary>
        /// The name of the prefix that was not found (<c>null</c> if unspecified).
        /// </summary>
        public virtual string Prefix { get { return prefix; } }

        /// <summary>
        /// Populates the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">A store containing information to be deserialized to a new instance.</param>
        /// <param name="context">Data describing the stream used to transfer serialized information.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Checker.NotNull(info, "info");
            base.GetObjectData(info, context);
            info.AddValue(PrefixInfoKey, prefix, typeof(string));
        }

        // Retrieves the prefix name formatted into the PrefixClauseFormat.
        private static string GetPrefixClause(string prefix)
        {
            return Environment.NewLine + PrefixClauseFormat.FormatStr(prefix);
        }
    }
}