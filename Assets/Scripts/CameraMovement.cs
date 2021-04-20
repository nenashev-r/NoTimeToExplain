using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float m_MoveSpeed;
        [SerializeField] private List<Waypoint> m_Waypoints;
        [SerializeField] private bool m_MoveToLast;

        private bool m_AtPlaceX = true;
        private bool m_AtPlaceY = true;

        private Vector3 m_Offset;
        private Vector3 m_ChangePos;

        private void Start()
        {
            if (m_Waypoints.Count > 0)
                m_Offset = transform.position - m_Waypoints[0].transform.position;
        }

        private void Update()
        {
            if (m_Waypoints == null || m_Waypoints.Count == 0)
                return;

            if (m_Waypoints[0].IsActive)
            {
                m_Waypoints.RemoveAt(0);

                if (m_Waypoints.Count == 0)
                    return;

                m_AtPlaceX = false;
                m_AtPlaceY = false;
            }

            if (!m_AtPlaceX || !m_AtPlaceY)
            {
                m_ChangePos = Vector3.zero;
                if (m_Waypoints[0].transform.position.x - transform.position.x > m_Offset.x)
                {
                    m_ChangePos.x = m_MoveSpeed * Time.deltaTime;
                }
                else m_AtPlaceX = true;
                if (m_Waypoints[0].transform.position.y - transform.position.y > m_Offset.y)
                {
                    m_ChangePos.y = m_MoveSpeed * Time.deltaTime;
                }
                else m_AtPlaceY = true;

                transform.position += m_ChangePos;
            }
        }
    }
}
