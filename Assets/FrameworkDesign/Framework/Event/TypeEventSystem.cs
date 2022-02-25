using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface ITypeEventSystem
    {
        void EventTrigger<T>() where T : new();
        void EventTrigger<T>(T e);
        IUnregister AddEventListener<T>(Action<T> action);
        void RemoveEventListener<T>(Action<T> action);
    }

    public interface IUnregister
    {
        void Unregister();
    }

    public class TypeEventSystemUnregister<T> : IUnregister
    {
        public ITypeEventSystem MyTypeEventSystem { get; set; }
        public Action<T> action { get; set; }
        public void Unregister()
        {
            MyTypeEventSystem.RemoveEventListener(action);
            MyTypeEventSystem = null;
            action = null;
        }
    }

    public class UnregisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnregister> m_Unregister = new HashSet<IUnregister>();

        public void AddUnregister(IUnregister unregister)
        {
            m_Unregister.Add(unregister);
        }

        private void OnDestroy()
        {
            foreach (var unregister in m_Unregister)
            {
                //Debug.Log(unregister.GetType() + "执行一次销毁");
                unregister.Unregister();
            }
            m_Unregister.Clear();
        }
    }

    public static class UnregisterExtension
    {
        public static void UnregisterWhenGameObjectDestroyed(this IUnregister unregister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnregisterOnDestroyTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnregisterOnDestroyTrigger>();
            }
            trigger.AddUnregister(unregister);

        }
    }

    public class TypeEventSystem : ITypeEventSystem
    {
        interface IRegistratons { }

        class Registrations<T> : IRegistratons
        {
            public Action<T> action = obj => { };
        }

        private Dictionary<Type, IRegistratons> m_EventRegistrations = new Dictionary<Type, IRegistratons>();

        public void EventTrigger<T>() where T : new()
        {
            var e = new T();
            EventTrigger(e);
        }

        public void EventTrigger<T>(T e)
        {
            var type = typeof(T);
            IRegistratons eventRegistrations;

            if (m_EventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                (eventRegistrations as Registrations<T>).action?.Invoke(e);
            }
        }

        public IUnregister AddEventListener<T>(Action<T> action)
        {
            //Debug.Log(typeof(T) + "执行一次注册");
            var type = typeof(T);
            IRegistratons eventRegistrations;

            if (m_EventRegistrations.TryGetValue(type, out eventRegistrations))
            {

            }
            else
            {
                eventRegistrations = new Registrations<T>();
                m_EventRegistrations.Add(type, eventRegistrations);
            }

            (eventRegistrations as Registrations<T>).action += action;

            return new TypeEventSystemUnregister<T>(){ action = action, MyTypeEventSystem = this};
        }

        public void RemoveEventListener<T>(Action<T> action)
        {
            var type = typeof(T);
            IRegistratons eventRegistrations;

            if (m_EventRegistrations.TryGetValue(type, out eventRegistrations))
            {
                (eventRegistrations as Registrations<T>).action -= action;
            }
        }
    }

}
