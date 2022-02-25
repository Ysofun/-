namespace FrameworkDesign
{
    public interface ICanTriggerEvent : ICanGetArchitecture { }

    public static class CanTriggerEventExtension
    {
        public static void EventTrigger<T>(this ICanTriggerEvent canTriggerEvent) where T : new()
        {
            canTriggerEvent.GetArchitecture().EventTrigger<T>();
        }

        public static void EventTrigger<T>(this ICanTriggerEvent canTriggerEvent, T e)
        {
            canTriggerEvent.GetArchitecture().EventTrigger(e);
        }
    }
}
