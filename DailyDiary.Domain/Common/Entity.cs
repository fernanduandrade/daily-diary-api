using System.ComponentModel.DataAnnotations.Schema;

namespace DailyDiary.Domain.Common;

public abstract class Entity
{
    [Column("id")]
    public Guid Id { get; set; }
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}