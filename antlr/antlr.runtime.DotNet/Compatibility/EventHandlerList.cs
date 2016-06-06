#if NETCORE
using System;

namespace antlr.runtime.Compatibility
{
    public class EventHandlerList : IDisposable
    {
        ListEntry head;
        Component parent;

        public EventHandlerList()
        {
        }

        internal EventHandlerList(Component parent)
        {
            this.parent = parent;
        }

        public Delegate this[object key]
        {
            get
            {
                ListEntry e = null;
                if (parent == null || parent.CanRaiseEventsInternal)
                {
                    e = Find(key);
                }
                if (e != null)
                {
                    return e.handler;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ListEntry e = Find(key);
                if (e != null)
                {
                    e.handler = value;
                }
                else
                {
                    head = new ListEntry(key, value, head);
                }
            }
        }

        public void AddHandler(object key, Delegate value)
        {
            ListEntry e = Find(key);
            if (e != null)
            {
                e.handler = Delegate.Combine(e.handler, value);
            }
            else
            {
                head = new ListEntry(key, value, head);
            }
        }

        /// <devdoc> allows you to add a list of events to this list </devdoc>
        public void AddHandlers(EventHandlerList listToAddFrom)
        {

            ListEntry currentListEntry = listToAddFrom.head;
            while (currentListEntry != null)
            {
                AddHandler(currentListEntry.key, currentListEntry.handler);
                currentListEntry = currentListEntry.next;
            }
        }

        public void Dispose()
        {
            head = null;
        }

        private ListEntry Find(object key)
        {
            ListEntry found = head;
            while (found != null)
            {
                if (found.key == key)
                {
                    break;
                }
                found = found.next;
            }
            return found;
        }

        public void RemoveHandler(object key, Delegate value)
        {
            ListEntry e = Find(key);
            if (e != null)
            {
                e.handler = Delegate.Remove(e.handler, value);
            }
            // else... no error for removal of non-existant delegate
            //
        }

        private sealed class ListEntry
        {
            internal ListEntry next;
            internal object key;
            internal Delegate handler;

            public ListEntry(object key, Delegate handler, ListEntry next)
            {
                this.next = next;
                this.key = key;
                this.handler = handler;
            }
        }

    }
}
#endif