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
        [SerializeField] private Vector2 m_JumpForce;
        [SerializeField] private float m_MoveSpeed;
        [SerializeField] private float m_RunMultiplier;
        [SerializeField] private float m_ClimbSpeed;

        private Transform m_Transform;
        private Vector2 m_CurVelocity;

        private bool m_IsGrounded = true;
        private string m_GroundLayer = "Ground";
        private float m_DownRayDistance = .45f;
        private float m_SideRayDistance = .29f;

        private float m_Modificater = 1;
        public float Modificater
        {
            set
            {
                m_Modificater = value;
            }
        }

        private int m_Side = 1;

        private bool m_StartClimb;
        private bool m_Climbing;
        private float m_ClimbHeight;

        private void Start()
        {
            m_Transform = transform;
            Stop();
        }

        private void Update()
        {
            GroundCheck();

            if (m_StartClimb || m_Climbing)
            {
                Climbing();
            }
            else if (m_IsGrounded)
                m_RigidBody.velocity = m_CurVelocity * m_Modificater;
        }

        private void GroundCheck()
        {
            var hit = Physics2D.Raycast(m_Transform.position, Vector2.down, m_DownRayDistance, 1 << LayerMask.NameToLayer(m_GroundLayer));
            
            if (hit.collider != null)
                m_IsGrounded = true;
            else
                m_IsGrounded = false;
        }

        private void Climbing()
        {
            if(m_StartClimb)
            {
                var hit = Physics2D.Raycast(m_Transform.position, Vector2.right*m_Side, m_SideRayDistance, 1 << LayerMask.NameToLayer(m_GroundLayer));

                if (hit.collider != null)
                {
                    m_StartClimb = false;
                    m_Climbing = true;

                    m_RigidBody.isKinematic = true;
                    m_RigidBody.velocity = Vector2.zero;
                }
                else
                    m_RigidBody.velocity = m_CurVelocity * m_Modificater;
            }            
            
            if(m_Climbing)
            {
                if (m_Transform.position.y < m_ClimbHeight)
                    m_Transform.position += Vector3.up * Time.deltaTime * m_ClimbSpeed * m_Modificater;
                else
                {
                    m_Climbing = false;

                    m_RigidBody.isKinematic = false;
                    m_RigidBody.AddForce(Vector2.right * m_Side * m_Modificater*1000);
                    Walk();
                }
            }

        }

        public void Climb(float height)
        {
            m_StartClimb = true;
            m_ClimbHeight = height;
        }

        public void TurnAround()
        {
            m_Side *= -1;

            m_CurVelocity.x *= m_Side;
        }

        public void Jump()
        {
            if (m_IsGrounded)
                m_RigidBody.AddForce(new Vector2(m_JumpForce.x * m_Side, m_JumpForce.y));
        }

        public void Walk()
        {
            m_CurVelocity = new Vector2(m_MoveSpeed * m_Side, m_RigidBody.velocity.y);
        }

        public void Run()
        {
            m_CurVelocity = new Vector2(m_MoveSpeed * m_RunMultiplier * m_Side, m_RigidBody.velocity.y);
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
    
