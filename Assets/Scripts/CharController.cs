using System;
using EventSystem;
using GameScripts.GlobalVariables;
using GameScripts.Interaction;
using UnityEngine;

namespace GameScripts
{
    public class CharController : MonoBehaviour
    {
        // Global variables
        [SerializeField] private GlobalFloat m_HitPoints;

        // Game events
        [Space]
        [SerializeField] private GameEvent m_DiedEvent;
        [SerializeField] private GameEvent m_SpawnedEvent;
        [SerializeField] private GameEvent m_HitObstacle;

        // References
        [Space]
        [SerializeField] private Rigidbody2D m_RigidBody;

        // Movement params
        [Space]
        [SerializeField] private float m_JumpForce;
        [SerializeField] private float m_MoveForce;
        [SerializeField] private float m_RunMultiplier;

        private Vector2 m_CurVelocity;

        private bool m_IsGrounded;
        private string m_GroundLayer = "Ground";
        private float m_RayDistance = .5f;

        private float m_Modificater = 1;
        public float Modificater
        {
            set
            {
                m_Modificater = value;
            }
        }

        private void Update()
        {
            GroundCheck();

            if (m_IsGrounded)
                m_RigidBody.velocity = m_CurVelocity * m_Modificater;
        }

        private void GroundCheck()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, m_RayDistance, 1 << LayerMask.NameToLayer(m_GroundLayer));
            
            if (hit.collider != null)
                m_IsGrounded = true;
            else
                m_IsGrounded = false;
        }

        public void Jump(float modificate = 1)
        {
            m_Modificater = modificate;
            if (m_IsGrounded)
                m_RigidBody.AddForce(Vector3.up * m_JumpForce * m_Modificater);
        }

        public void Walk(float modificate = 1)
        {
            m_Modificater = modificate;
            m_CurVelocity = new Vector2(m_MoveForce * m_Modificater, m_RigidBody.velocity.y);
        }

        public void Run(float modificate = 1)
        {
            m_Modificater = modificate;
            m_CurVelocity = new Vector2(m_MoveForce * m_Modificater * m_RunMultiplier, m_RigidBody.velocity.y);
        }

        public void Stop()
        {
            m_CurVelocity = Vector2.zero;
        }

        public void AddDamage(float damage)
        {
            float hp = m_HitPoints.value;
            if (damage >= hp)
            {
                m_HitPoints.value = 0;
                m_HitObstacle.Invoke();
                Die();
                return;
            }

            hp -= damage;
            m_HitPoints.value = hp;
            m_HitObstacle.Invoke();
        }

        private void Die()
        {
            m_DiedEvent.Invoke();
            gameObject.SetActive(false);
        }
    }
}
    
