using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent m_Event;
        public UnityEvent Response;

        private void OnEnable()
        {
            m_Event.RegisterListener(this);
        }
        
        private void OnDisable()
        {
            m_Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}