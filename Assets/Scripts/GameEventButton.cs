using UnityEngine;
using EventSystem;
using UnityEngine.UI;

namespace GameScripts
{
    public class GameEventButton : MonoBehaviour
    {
        private Button m_Btn;

        public GameEvent Event { get; set; }

        private void OnEnable()
        {
            if (Event == null)
                return;

            if (m_Btn == null)
                m_Btn = GetComponent<Button>();

            if (m_Btn == null)
                return;

            m_Btn.onClick.AddListener(AddEventToWaypoint);
        }

        private void OnDisable()
        {
            if (Event == null || m_Btn == null)
                return;

            m_Btn.onClick.RemoveListener(AddEventToWaypoint);
        }

        private void AddEventToWaypoint()
        {
            ActionsPanel.Instance.CurWaypoint.AddEvents(Event);
        }
    }
}