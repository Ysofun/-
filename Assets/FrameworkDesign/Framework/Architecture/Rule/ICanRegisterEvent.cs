using System;

namespace FrameworkDesign
{
    public interface ICanRegisterEvent : ICanGetArchitecture { }
    public static class CanRegisterEventExtension
    {
        public static IUnregister AddEventListener<T>(this ICanRegisterEvent canRegisterEvent, Action<T> action)
        {
            return canRegisterEvent.GetArchitecture().AddEventListener(action);
        }
        public static void RemoveEventListener<T>(this ICanRegisterEvent canRegisterEvent, Action<T> action)
        {
            canRegisterEvent.GetArchitecture().RemoveEventListener(action);
        }
    }

}
