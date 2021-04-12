using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float m_MoveSpeed;
        [SerializeField] private List<Waypoint> m_Waypoints;

        private bool m_AtPlace = true;

        private void Update()
        {
            if (m_Waypoints == null || m_Waypoints.Count == 0)
                return;

            if (m_Waypoints[0].IsActive)
            {
                m_Waypoints.RemoveAt(0);

                if (m_Waypoints.Count == 0)
                    return;

                m_AtPlace = false;
            }

            if (!m_AtPlace)
            {
                transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;

                if (transform.position.x - m_Waypoints[0].transform.position.x > 0)
                    m_AtPlace = true;
            }
        }
    }
}
