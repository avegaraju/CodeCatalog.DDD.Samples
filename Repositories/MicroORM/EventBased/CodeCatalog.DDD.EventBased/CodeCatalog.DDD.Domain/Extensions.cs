using System.Collections.Generic;

namespace CodeCatalog.DDD.Domain
{
    public static class Extensions
    {
        public static IEnumerable<IEvent> DequeueAll(this Queue<IEvent> queue)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                yield return queue.Dequeue();
            }
        }
    }
}
