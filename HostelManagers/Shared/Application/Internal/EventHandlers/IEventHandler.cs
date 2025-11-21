using Cortex.Mediator.Notifications;
using HostelManagers.Shared.Domain.Model.Events;

namespace HostelManagers.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}