**Error Handling Library Based on FluentResults**  

This library extends **FluentResults** by introducing a system of typed errors with predefined metadata structures for common scenarios. It implements the "Result-based error handling" pattern with enhanced semantics and error granularity.  

---

### **Key Features**  
1. **Standardized Error Types**  
   Includes ready-to-use implementations for common cases:  
   - `ValidationError` — Object validation errors with nested field errors (`ValidationFieldError`)  
   - `NotNullError` — Violation of a "not null" constraint  
   - `NotFoundError` — Missing entity  
   - Custom errors via inheritance from `BaseError`  

2. **Structured Metadata**  
   Each error contains:  
   - Error type (`ErrorType`) — Classification (Validation, NotNull, etc.)  
   - Technical details in `Metadata`:  
     - Error code (`ErrorCode`)  
     - Field/entity name  
     - Invalid value  
     - Documentation reference  
     - Additional parameters (see `ErrorMetadataType`)  

3. **Error Hierarchy**  
   - Base class `BaseError` implements FluentResults' `IError`  
   - Support for nested errors via the `Reasons` collection  

---

### **Usage Example**  
```csharp  
// Object validation  
var errors = new List<ValidationFieldError>  
{  
    new ValidationFieldError(  
        message: "Invalid email format",  
        errorCode: "INVALID_EMAIL",  
        fieldName: "Email",  
        attemptedValue: "user@"  
    ),  
    new NotNullError("Username")  
};  

var validationResult = Result.Fail(new ValidationError("User", errors));  

// Error handling  
if (validationResult.IsFailed)  
{  
    foreach (var error in validationResult.Errors)  
    {  
        if (error is ValidationError ve)  
        {  
            Console.WriteLine($"Validation error for {ve.Metadata["EntityType"]}");  
            foreach (var fieldError in ve.Reasons.Cast<ValidationFieldError>())  
            {  
                Console.WriteLine($"{fieldError.Metadata["FieldName"]}: {fieldError.Message}");  
            }  
        }  
    }  
}  
```  

---

### **Integration with FluentResults**  
- Compatible with all FluentResults features (result chaining, error pipeline handling)  
- Errors are automatically serialized into FluentResults' standard structures  

---

### **Benefits**  
- **Unified error style** across application layers  
- **Prebuilt templates** for common scenarios  
- **Detailed error analysis** via structured metadata  
- **Easy extensibility** (create new error types via inheritance)  

---

### **Extension Guidelines**

1. **Adding Specific Errors**  
Create error classes by inheriting from `BaseError`:
```csharp
public sealed class DatabaseError : BaseError 
{ 
    // Constructor implementation
    public DatabaseError(...) : base(...) { ... } 
}
```

2. **Error Constructor**  
When implementing a constructor:
- Pass parameters to the parent class via `base()`
- Use the `CreateMetadata()` method to build the metadata dictionary

**Required parameters**:
- `string message` — User-friendly error message
- `ErrorType errorType` — Error type from the `ErrorType` enumeration

**Optional parameters**:
- `IEnumerable<IError>? reasons = null` — List of errors that caused the current one
- `Dictionary<string, object>? metadata = null` — Technical details about the error. Can be passed via the `CreateMetadata()` method:
    ```csharp
    SomeError(
        ...,
        metadata: CreateMetadata(
            (ErrorMetadataType.FieldName, "SOME_FIELD"),
            (ErrorMetadataType.ErrorCode, "CUSTOM_CODE")
        )
    )
    ``` 
