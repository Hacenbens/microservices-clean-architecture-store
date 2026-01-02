namespace ProductService.Domain.Comman.IEntities;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected AuditableEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}