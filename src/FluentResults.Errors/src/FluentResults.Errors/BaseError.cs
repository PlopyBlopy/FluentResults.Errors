namespace FluentResults.Errors
{
    /// <summary>
    /// Represents a base error class implementing the IError interface.
    /// Provides common error properties and metadata handling capabilities.
    /// </summary>
    public abstract class BaseError : IError
    {
        /// <summary>
        /// Gets the human-readable error message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the list of underlying errors that caused this error
        /// </summary>
        public List<IError> Reasons { get; } = new List<IError>();

        /// <summary>
        /// Gets the dictionary of additional error metadata.
        /// Contains technical details about the error context.
        /// </summary>
        public Dictionary<string, object> Metadata { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the BaseError class
        /// </summary>
        /// <param name="message">Human-readable error description</param>
        /// <param name="errorType">Categorization of the error type</param>
        /// <param name="reasons">Optional collection of underlying errors</param>
        /// <param name="metadata">Optional metadata dictionary. Note: 'errorType' key is reserved.</param>
        /// <exception cref="ArgumentException">Thrown when metadata contains reserved 'errorType' key</exception>
        protected BaseError(string message, ErrorType errorType, IEnumerable<IError>? reasons = null, Dictionary<string, object>? metadata = null)
        {
            Message = message;
            Metadata.Add("errorType", errorType);

            if (reasons != null)
                Reasons = reasons.ToList();

            if (metadata != null)
            {
                if (metadata.ContainsKey("errorType"))
                    throw new ArgumentException("Key 'errorType' is reserved.");
                else
                {
                    foreach (var item in metadata)
                    {
                        Metadata.Add(item.Key, item.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a metadata dictionary from provided key-value pairs
        /// </summary>
        /// <param name="values">Array of ErrorMetadataType-value tuples</param>
        /// <returns>Dictionary with string keys (from enum names) and object values</returns>
        protected static Dictionary<string, object> CreateMetadata(params (ErrorMetadataType Key, object Value)[] values)
        {
            var dict = new Dictionary<string, object>();
            foreach (var item in values)
            {
                dict.Add(item.Key.ToString(), item.Value);
            }
            return dict;
        }
    }
}