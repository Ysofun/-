using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public struct EventA
    {

    }

    public struct EventB
    {
        public int paramB;
    }

    public interface IEventGroup { }

    public class EventC : IEventGroup
    {

    }

    public class EventD : IEventGroup
    {

    }

    public class TypeEventSystemExample : MonoBehaviour
    {
        private ITypeEventSystem m_TypeEventSystem = null;

        private void Start()
        {
            m_TypeEventSystem = new TypeEventSystem();
            m_TypeEventSystem.AddEventListener<EventA>(eA =>
            {
                Debug.Log("OnEventA");
            }).UnregisterWhenGameObjectDestroyed(gameObject);

            m_TypeEventSystem.AddEventListener<EventB>(onEventB);

            m_TypeEventSystem.AddEventListener<IEventGroup>(group =>
            {
                Debug.Log(group.GetType());
            }).UnregisterWhenGameObjectDestroyed(gameObject);
        }

        private void onEventB(EventB obj)
        {
            Debug.Log("OnEventB" + obj.paramB);
            transform.Find("GameObject").gameObject.GetComponent<NewEventA>().DestroySelf();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_TypeEventSystem.EventTrigger<EventA>();
            }

            if (Input.GetMouseButtonDown(1))
            {
                m_TypeEventSystem.EventTrigger(new EventB() { paramB = 123 });
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_TypeEventSystem.EventTrigger<IEventGroup>(new EventC());
                m_TypeEventSystem.EventTrigger<IEventGroup>(new EventD());
            }
        }

        private void OnDestroy()
        {
            m_TypeEventSystem.RemoveEventListener<EventB>(onEventB);
            m_TypeEventSystem = null;
        }
    }

}
