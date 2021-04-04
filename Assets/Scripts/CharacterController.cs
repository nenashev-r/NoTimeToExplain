using System;
using EventSystem;
using GameScripts.GlobalVariables;
using GameScripts.Interaction;
using UnityEngine;

namespace GameScripts
{
    public class CharacterController : MonoBehaviour, IDamageable
    {
        // Global variables
        [SerializeField] private GlobalFloat m_HitPoints;
        
        // Game events
        [SerializeField][Space] private GameEvent m_DiedEvent;
        [SerializeField] private GameEvent m_SpawnedEvent;
        [SerializeField] private GameEvent m_HitObstacle;
        
        // References
        [SerializeField][Space] private Rigidbody2D m_RigidBody;
        
        // Movement params
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
    
