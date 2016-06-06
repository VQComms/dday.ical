#if NETCORE
using System;
using System.ComponentModel;

namespace antlr.runtime.Compatibility
{
    public class Component : IComponent
    {
        private static readonly object EventDisposed = new object();

        private ISite site;
        private EventHandlerList events;

        ~Component()
        {
            Dispose(false);
        }

        protected virtual bool CanRaiseEvents
        {
            get
            {
                return true;
            }
        }


        internal bool CanRaiseEventsInternal
        {
            get
            {
                return CanRaiseEvents;
            }
        }

        public event EventHandler Disposed
        {
            add
            {
                Events.AddHandler(EventDisposed, value);
            }
            remove
            {
                Events.RemoveHandler(EventDisposed, value);
            }
        }

        protected EventHandlerList Events
        {
            get
            {
                if (events == null)
                {
                    events = new EventHandlerList(this);
                }
                return events;
            }
        }

        public virtual ISite Site
        {
            get { return site; }
            set { site = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    if (site != null && site.Container != null)
                    {
                        site.Container.Remove(this);
                    }
                    if (events != null)
                    {
                        EventHandler handler = (EventHandler)events[EventDisposed];
                        if (handler != null) handler(this, EventArgs.Empty);
                    }
                }
            }
        }

        public IContainer Container
        {
            get
            {
                ISite s = site;
                return s == null ? null : s.Container;
            }
        }

        protected virtual object GetService(Type service)
        {
            ISite s = site;
            return ((s == null) ? null : s.GetService(service));
        }

        protected bool DesignMode
        {
            get
            {
                ISite s = site;
                return (s == null) ? false : s.DesignMode;
            }
        }

        public override String ToString()
        {
            ISite s = site;

            if (s != null)
                return s.Name + " [" + GetType().FullName + "]";
            else
                return GetType().FullName;
        }
    }
}
#endif