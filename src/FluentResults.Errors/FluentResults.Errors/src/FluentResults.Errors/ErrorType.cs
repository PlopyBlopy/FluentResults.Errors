namespace FluentResults.Errors
{
    /// <summary>
    /// Defines types of errors in the system
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// General validation error
        /// </summary>
        Validation = 0,

        /// <summary>
        /// Field-specific validation error
        /// </summary>
        ValidationField = 1,

        /// <summary>
        /// Resource not found error
        /// </summary>
        NotFound = 2,

        /// <summary>
        /// Missing required value error
        /// </summary>
        NotNull = 3,
    }
}