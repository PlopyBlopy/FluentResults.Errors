namespace FluentResults.Errors.Tests.Unit
{
    public class ValidationErrorTests
    {
        [Fact]
        public void Ctor_WithMessageAndErrors_ShouldCreateValidationErrorWithReason()
        {
            // Arrange
            ValidationFieldError validationFieldError0 = new ValidationFieldError("Title cannot be empty. 0", "NotEmptyValidator 0", "Title 0", "");
            ValidationFieldError validationFieldError1 = new ValidationFieldError("Title cannot be empty. 1", "NotEmptyValidator 1", "Title 1", "");
            ValidationFieldError validationFieldError2 = new ValidationFieldError("Title cannot be empty. 2", "NotEmptyValidator 2", "Title 2", "");
            List<ValidationFieldError> errors = new List<ValidationFieldError> { validationFieldError0, validationFieldError1, validationFieldError2 };

            // Act
            ValidationError validationError = new ValidationError("ProductTest", errors);

            // Assert
            Assert.True(validationError.Reasons.Any());
            Assert.IsType<ValidationFieldError>(validationError.Reasons[0]);
            Assert.Equal("Validation error.", validationError.Message);
            Assert.Equal(ErrorType.Validation, validationError.ErrorType);
        }
    }
}