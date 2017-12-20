using System.Collections.Generic;

namespace CodeCatalog.DDD.Domain
{
    public class AggregateBase
    {
        private readonly Queue<IEvent> _eventsQueue = new Queue<IEvent>();

        public void EnqueueEvent(IEvent @event)
        {
            _eventsQueue.Enqueue(@event);
        }

        public IEnumerable<IEvent> DequeueEvents()
        {
            return _eventsQueue.DequeueAll();
        }
    }
}
