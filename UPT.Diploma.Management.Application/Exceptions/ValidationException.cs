using FluentValidation.Results;

namespace UPT.Diploma.Management.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public List<string> ValidationErrors { get; } = new List<string>();
    
    public ValidationException(ValidationResult validationResult)
    {
        foreach (var err in validationResult.Errors)
        {
            ValidationErrors.Add(err.ErrorMessage);
        }
    }

    public ValidationException(string validationError)
    {
        ValidationErrors.Add(validationError);
    }

}