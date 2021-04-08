using EventSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    [System.Serializable]
    public class EventButtonData
    {
        public GameEvent Event;
        public Button Button;
    }

    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private List<EventButtonData> m_Events;

        private void OnEnable()
        {
            m_Events.ForEach(e => e.Button.onClick.AddListener(e.Event.Invoke));
        }
        
        private void OnDisable()
        {
            m_Events.ForEach(e => e.Button.onClick.RemoveListener(e.Event.Invoke));
        }
    }
}