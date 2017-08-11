using System.Collections;
using System.Collections.Generic;

public class EventManager {

    public struct EventEntry
    {

        public delegate void Notify(string message = "");

        public event Notify OnNotify;

        public void NotifyAll(string message = "")
        {
            if (this.OnNotify != null)
            {
                this.OnNotify(message);
            }
        }
    }

    protected static Dictionary<string, EventEntry> entries = new Dictionary<string, EventEntry>();

    /**
     * Create Event Entry
     */
    protected static void CreateEntry(string name)
    {
        if (!entries.ContainsKey(name))
        {
            entries.Add(name, new EventEntry());
        }
    }

    /**
     * Subscribe to an Event
     */
	public static void Subscribe(string name, EventEntry.Notify callback)
    {
        CreateEntry(name);

        entries[name].OnNotify += callback;
    }

    /**
     * Unsubscribe from an Event
     */
    public static void Unsubscribe(string name, EventEntry.Notify callback)
    {
        if (!entries.ContainsKey(name)) return;

        entries[name].OnNotify -= callback;
    }

    /**
     * Notify an Event
     */
    public static void Notify(string name, string message = "")
    {
        if (!entries.ContainsKey(name)) return;

        entries[name].NotifyAll(message);
    }

    /**
     * Remove an Event
     */
    public static void Remove(string name)
    {
        Notify("event.remove", name);

        entries.Remove(name);
    }

    /**
     * Clear Events
     */
    public static void Clear()
    {
        Notify("event.clear");

        entries.Clear();
    }
}
