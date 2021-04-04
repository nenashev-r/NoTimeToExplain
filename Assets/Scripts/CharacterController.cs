using System;
using EventSystem;
using GameScripts.GlobalVariables;
using UnityEngine;

namespace GameScripts
{
    public class CharacterController : MonoBehaviour
    {
        // Global variables
        [SerializeField] private GlobalFloat m_HitPoints;
        
        // Game events
        [SerializeField][Space] private GameEvent m_DiedEvent;
        [SerializeField] private GameEvent m_SpawnedEvent;
        
        // References
        [SerializeField][Space] private Rigidbody m_RigidBody;
        
        // Movement
        [SerializeField] [Space] private float m_JumpForce;
        [SerializeField] private float m_MoveForce;
        [SerializeField] private float m_MaxSpeed;


        private void Update()
        {
            m_RigidBody.velocity = Vector3.ClampMagnitude(m_RigidBody.velocity, m_MaxSpeed);
        }

        public void Jump()
        {
            m_RigidBody.AddForce(Vector3.up * m_JumpForce);
        }

        public void MoveLeft()
        {
            m_RigidBody.AddForce(-Vector3.right * m_MoveForce);
        }

        public void MoveRight()
        {
            m_RigidBody.AddForce(Vector3.right * m_MoveForce);
        }
    }
}
    
