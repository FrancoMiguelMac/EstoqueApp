using FluentValidation;

namespace Estoque.Domain.Entities.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome tem que ser preenchido")
                .Length(0, 40).WithMessage("Tamanho do campo nome excedido");

            RuleFor(c => c.Value)
                .NotNull().WithMessage("Valor tem que ser preenchido");
        }
    }
}
