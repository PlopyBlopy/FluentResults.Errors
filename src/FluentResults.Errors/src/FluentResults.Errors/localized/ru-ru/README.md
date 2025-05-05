**Описание библиотеки ошибок на базе FluentResults**

Библиотека расширяет возможности **FluentResults**, добавляя систему типизированных ошибок с предопределенной структурой метаданных для стандартных сценариев. Реализует паттерн "Result-based error handling" с улучшенной семантикой и детализацией ошибок.

---

### **Ключевые особенности**
1. **Стандартизированные типы ошибок**  
   Включает готовые реализации для частых кейсов:
   - `ValidationError` — ошибка валидации объекта с вложенными ошибками полей (`ValidationFieldError`)
   - `NotNullError` — нарушение требования "не null"
   - `NotFoundError` — отсутствие сущности
   - Кастомные ошибки через наследование от `BaseError`

2. **Структурированные метаданные**  
   Каждая ошибка содержит:
   - Тип ошибки (`ErrorType`) — классификация (Validation, NotNull, и т.д.)
   - Технические детали в `Metadata`:
     - Код ошибки (`ErrorCode`)
     - Имя поля/сущности
     - Некорректное значение
     - Ссылка на документацию
     - Другие параметры (см. `ErrorMetadataType`)

3. **Иерархия ошибок**  
   - Базовый класс `BaseError` реализует `IError` из FluentResults
   - Поддержка вложенных ошибок через коллекцию `Reasons`

---

### **Пример использования**
```csharp
// Валидация объекта
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

// Обработка
if (validationResult.IsFailed)
{
    foreach (var error in validationResult.Errors)
    {
        if (error is ValidationError ve)
        {
            Console.WriteLine($"Ошибка валидации {ve.Metadata["EntityType"]}");
            foreach (var fieldError in ve.Reasons.Cast<ValidationFieldError>())
            {
                Console.WriteLine($"{fieldError.Metadata["FieldName"]}: {fieldError.Message}");
            }
        }
    }
}
```

---

### **Интеграция с FluentResults**
- Совместима со всеми возможностями FluentResults (комбинация результатов, обработка цепочек ошибок)
- Ошибки автоматически сериализуются в стандартные структуры FluentResults

---

### **Преимущества**
- **Единый стиль ошибок** во всех слоях приложения
- **Готовые шаблоны** для частых сценариев
- **Детальный анализ** ошибок через метаданные
- **Лёгкое расширение** (создание новых типов ошибок через наследование)

---

### **Рекомендации по расширению**

1. **Добавление специфичных ошибок**  
Создавайте классы ошибок, наследуясь от `BaseError`:
```csharp
public sealed class DatabaseError : BaseError 
{ 
    // Реализация конструктора
    public DatabaseError(...) : base(...) { ... } 
}
```

2. **Конструктор ошибки**  
При реализации конструктора:
- Передавайте параметры в родительский класс через `base()`
- Используйте метод `CreateMetadata()` из `BaseError` для формирования словаря метаданных

**Обязательные параметры**:
- `string message` — понятное пользователю сообщение об ошибке
- `ErrorType errorType` — тип ошибки из перечисления `ErrorType`

**Необязательные параметры**:
- `IEnumerable<IError>? reasons = null` — список ошибок, вызвавших текущую
- `Dictionary<string, object>? metadata = null` — технические подробности об ошибке. Можно передать через метод `CreateMetadata()`:
    ```csharp
    SomeError(
        ...,
        metadata: CreateMetadata(
            (ErrorMetadataType.FieldName, "SOME_FIELD"),
            (ErrorMetadataType.ErrorCode, "CUSTOM_CODE")
        )
    )
    ```