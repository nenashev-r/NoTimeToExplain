using EventSystem;
using System.Collections.Generic;
using UnityEngine;

public class WaypointEventsPanel : MonoBehaviour
{
    [SerializeField] private GameObject m_IconPrefab;
    [SerializeField] private float m_Offset = 0.02f;
    [SerializeField] private int m_Columns = 3;
    [SerializeField] private GameObject m_Background;

    private float m_IconSize = 0.32f;

    private List<GameObject> m_CurIcons;

    public void ShowEvents(GameEvent[] events)
    {
        if (m_CurIcons == null)
            m_CurIcons = new List<GameObject>();
        else
            m_CurIcons.ForEach(o => o.SetActive(false));

        if (events == null || events.Length == 0)
        {
            m_Background.SetActive(false);
            return;
        }

        m_Background.SetActive(true);

        if (m_CurIcons.Count >= events.Length)
        {
            for (int i = 0; i < events.Length; i++)
            {
                m_CurIcons[i].GetComponentInChildren<TMPro.TextMeshPro>().text = $"{events[i].name[0]}";
                m_CurIcons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < m_CurIcons.Count; i++)
            {
                m_CurIcons[i].GetComponentInChildren<TMPro.TextMeshPro>().text = $"{events[i].name[0]}";
                m_CurIcons[i].SetActive(true);
            }
            for (int i = m_CurIcons.Count; i < events.Length; i++)
            {
                var icon = Instantiate(m_IconPrefab, transform);
                icon.transform.localPosition = Vector3.zero;
                icon.GetComponentInChildren<TMPro.TextMeshPro>().text = $"{events[i].name[0]}";
                m_CurIcons.Add(icon);
            }
        }

        var count = (events.Length > m_Columns) ? m_Columns : events.Length;
        float of = (count % 2 == 0) ? 0.5f : 0;

        int col = 0;
        int row = (events.Length % m_Columns == 0) ? (events.Length - 1) / m_Columns : events.Length / m_Columns;

        m_Background.transform.localScale = new Vector3(count * 1.1f, (row + 1) * 1.1f, 1);
        m_Background.transform.localPosition = new Vector3(0, row * .17f, 0);

        for (int i = 0; i < events.Length; i++)
        {            
            m_CurIcons[i].transform.localPosition = new Vector3((col - (int)(count * 0.5f) + of) * (m_IconSize + m_Offset),
                                                                row * (m_IconSize + m_Offset), 0);

            if (++col >= m_Columns)
            {
                col = 0;
                row--;
            }            
        }
    }
}
