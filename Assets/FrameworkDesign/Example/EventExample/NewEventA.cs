using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class NewEventA : MonoBehaviour
    {
        private ITypeEventSystem m_TypeEventSystem = null;

        void Start()
        {
            m_TypeEventSystem = new TypeEventSystem();
            m_TypeEventSystem.AddEventListener<EventA>(eA =>
            {
                Debug.Log("OnNewEventA");
            }).UnregisterWhenGameObjectDestroyed(gameObject);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }

}
