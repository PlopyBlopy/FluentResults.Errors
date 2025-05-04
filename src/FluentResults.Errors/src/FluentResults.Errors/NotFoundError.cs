namespace FluentResults.Errors
{
    /// <summary>
    /// Represents an error when a requested entity is not found
    /// </summary>
    public sealed class NotFoundError : BaseError
    {
        /// <summary>
        /// Initializes a new not-found error
        /// </summary>
        /// <param name="errorCode">Domain-specific error code</param>
        /// <param name="entityName">Name of the missing entity</param>
        public NotFoundError(string errorCode, string entityName)
            : base(
                message: $"The '{entityName}' not found.",
                errorType: ErrorType.NotFound,
                reasons: null,
                metadata: CreateMetadata(
                    (ErrorMetadataType.ErrorCode, "NOT_FOUND"),
                    (ErrorMetadataType.EntityType, entityName)
                ))
        {
        }
    }
}