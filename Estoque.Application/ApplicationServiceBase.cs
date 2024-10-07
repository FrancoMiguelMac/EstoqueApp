using FluentValidation.Results;

namespace Estoque.Application
{
    public abstract class ApplicationServiceBase
    {
        public ValidationResult ValidationResult { get; protected set; } = new ValidationResult();

        public void AddValidationError(string error, string description)
        {
            ValidationResult.Errors.Add(new ValidationFailure(error, description));
        }
    }
}
