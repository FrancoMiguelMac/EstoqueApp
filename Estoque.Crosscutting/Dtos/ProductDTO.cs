namespace Estoque.Crosscutting.Dtos
{
    public record ProductDTO(Guid? Id, string Name, decimal Value, DateTime CreatedAt, DateTime ModifiedDate)
    {
    }
}
