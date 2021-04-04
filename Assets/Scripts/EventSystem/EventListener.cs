using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField] private List<Events> m_Events;

        private void OnEnable()
        {
            m_Events.ForEach(e=>e.m_Event.RegisterListener(this));
        }
        
        private void OnDisable()
        {
            m_Events.ForEach(e => e.m_Event.UnregisterListener(this));
        }

        public void OnEventRaised(GameEvent ev)
        {
            m_Events.ForEach(e => 
            {
                if (e.m_Event == ev)
                    e.Response.Invoke();
            });
        }
    }

    [System.Serializable]
    public class Events
    {
        public GameEvent m_Event;
        public UnityEvent Response;
    }
}