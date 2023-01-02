
namespace Services.EventsSystem
{
    public delegate void EventAction();
    public delegate void EventAction<T>(T value);
}