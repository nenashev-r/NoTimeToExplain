using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu( menuName = "GameEvents/SimpleEvent")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField] private float m_Distance = 1;

        [HideInInspector] public List<EventListener> m_EventListeners;

        public float Distance => m_Distance;

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
                eventListener.OnEventRaised(this);
            }
        }
    }
}
