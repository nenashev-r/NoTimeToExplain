using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        [HideInInspector] public List<EventListener> m_EventListeners;
    
        public void RegisterListener(EventListener listener)
        {
            m_EventListeners.Add(listener);
        }

        public void UnregisterListener(EventListener listener)
        {
            m_EventListeners.Remove(listener);
        }

        public void Invoke()
        {
            foreach (var eventListener in m_EventListeners)
            {
                eventListener.OnEventRaised();
            }
        }
    }
}
