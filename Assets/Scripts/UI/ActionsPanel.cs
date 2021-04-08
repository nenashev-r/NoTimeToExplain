using EventSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class ActionsPanel : MonoBehaviour
    {
        [SerializeField] private GameEventButton m_PrefabBtn;

        [SerializeField] private List<GameEvent> m_AvailableEvents;

        public static ActionsPanel Instance;

        public Waypoint CurWaypoint { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;

            GenerateButtons();
            gameObject.SetActive(false);
        }

        private void GenerateButtons()
        {
            if (m_PrefabBtn == null || m_AvailableEvents.Count == 0)
                return;

            foreach (var ev in m_AvailableEvents)
            {
                var obj = Instantiate(m_PrefabBtn.gameObject, transform);
                obj.name = $"{ev.name}_Btn";
                obj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ev.name;
                obj.GetComponent<GameEventButton>().Event = ev;
            }
        }

        public void Switch(Waypoint swithcBy)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (gameObject.activeSelf)
                CurWaypoint = swithcBy;
            else
                CurWaypoint = null;
        }
    }
}

