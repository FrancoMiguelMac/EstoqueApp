namespace Estoque.Domain.Core.Models
{
    public abstract class AuditEntity : Entity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
