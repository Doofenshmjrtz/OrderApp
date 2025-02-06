using Domain.Core.Common.Abstractions;

namespace Domain.Core.Order.Events;

public sealed record OrderCreatedDomainEvent(Guid OrderId) : IDomainEvent;