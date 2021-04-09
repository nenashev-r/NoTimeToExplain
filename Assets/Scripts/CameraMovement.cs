using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> m_Waypoints;

        private Vector3 m_Offset;
        void Start()
        {
            m_Offset = m_Waypoints[0].transform.position - transform.position;
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
            }

            transform.position = m_Waypoints[0].transform.position - m_Offset;
        }
    }
}
