using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private Vector3 m_Offset;
    void Start()
    {
        m_Offset = m_Target.position - transform.position;
    }
    
    private void Update()
    {
        transform.position = m_Target.position - m_Offset;
    }
}
