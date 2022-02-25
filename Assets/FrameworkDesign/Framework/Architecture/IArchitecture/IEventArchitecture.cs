using System;

namespace FrameworkDesign
{
    public interface IEventArchitecture
    {
        void EventTrigger<T>() where T : new();
        void EventTrigger<T>(T e);
        IUnregister AddEventListener<T>(Action<T> action);
        void RemoveEventListener<T>(Action<T> action);
    }
}
