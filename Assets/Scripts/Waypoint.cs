using EventSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameScripts
{
    public class Waypoint : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private WaypointEventsPanel m_EventsPanel;
        [SerializeField] private int m_MaxEvents = -1;

        private Queue<GameEvent> m_Events;

        private bool m_IsActive;

        public static Waypoint ActiveWaypoint { get; private set; }

        private void Update()
        {
            if (!m_IsActive)
                return;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (ActiveWaypoint != null)
                ActiveWaypoint.ClearEvents();

            ActiveWaypoint = this;

            m_IsActive = true;

            TakeAnotherAction().Invoke();
        }

        private GameEvent TakeAnotherAction()
        {
            if (m_Events != null && m_Events.Count > 0)
                return m_Events.Dequeue();

            return null;
        }

        private void ClearEvents()
        {
            m_Events.Clear();
            m_IsActive = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ActionsPanel.Instance?.Switch(this);

            m_EventsPanel?.Switch();
        }

        public void AddEvents(GameEvent ev)
        {
            if (m_Events == null)
                m_Events = new Queue<GameEvent>();

            if (m_MaxEvents > 0 && m_MaxEvents <= m_Events.Count)
                return;

            m_Events.Enqueue(ev);

            m_EventsPanel?.ShowEvents(m_Events.ToArray());
        }
    }
}
