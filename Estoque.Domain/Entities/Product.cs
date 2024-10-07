using Estoque.Domain.Core.Models;
using Estoque.Domain.Entities.Validations;

namespace Estoque.Domain.Entities
{
    public class Product : AuditEntity
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new ProductValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        /// <summary>
        /// Object instance.
        /// </summary>
        public Product(string name, decimal value)
        {
            Name = name;
            Value = value;
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }

        public void Update(string name, decimal value)
        {
            Name = name;
            Value = value;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
