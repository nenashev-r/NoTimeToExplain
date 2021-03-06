using EventSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameScripts
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private WaypointEventsPanel m_EventsPanel;
        [Tooltip("negative value means infinity")]
        [SerializeField] private int m_MaxEvents = -1;
        [SerializeField] private Clickable m_ClearBtn;
        [SerializeField] private Clickable m_IconBtn;

        private Queue<GameEvent> m_Events;

        public bool IsActive { get; private set; }

        private void Start()
        {
            m_ClearBtn.gameObject.SetActive(false);
            m_IconBtn.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<BehaviorController>()?.SetEventsQueue(m_Events);

            IsActive = true;
            m_IconBtn.enabled = false;

            if (ActionsPanel.Instance != null && ActionsPanel.Instance.gameObject.activeSelf)
                ActionsPanel.Instance.SwitchOff(this);
        }

        private void ClearEvents()
        {
            m_Events.Clear();
            m_EventsPanel?.ShowEvents(null);

            m_ClearBtn.gameObject.SetActive(false);
            m_ClearBtn.OnClick.RemoveListener(ClearEvents);
        }

        public void AddEvents(GameEvent ev)
        {
            if (m_Events == null)
                m_Events = new Queue<GameEvent>();

            if (m_MaxEvents > 0 && m_MaxEvents <= m_Events.Count)
                return;

            m_Events.Enqueue(ev);

            if(!m_ClearBtn.gameObject.activeSelf)
            {
                m_ClearBtn.gameObject.SetActive(true);
                m_ClearBtn.OnClick.AddListener(ClearEvents);
            }

            m_EventsPanel?.ShowEvents(m_Events.ToArray());
        }

        public void StartLevel()
        {
            m_IconBtn.enabled = true;
        }
        public void FinishLevel()
        {
            m_IconBtn.enabled = false;
        }
    }
}
