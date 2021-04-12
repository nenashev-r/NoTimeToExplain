using System.Linq;
using System.Collections.Generic;
using EventSystem;
using GameScripts.GlobalVariables;
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

        [Space]
        [SerializeField] private List<LayerRayChecker> m_GroundCheckers;
        [SerializeField] private LayerRayChecker m_WallRightChecker;
        [SerializeField] private LayerRayChecker m_WallLeftChecker;


        private Transform m_Transform;
        private Vector2 m_CurVelocity;

        private bool m_IsGrounded => m_GroundCheckers != null && m_GroundCheckers.Count > 0 && m_GroundCheckers.Any(o => o.IsLayer);

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
        private bool m_AfterClimb;
        private float m_ClimbHeight;

        private bool m_AfterJump;

        private void Start()
        {
            m_Transform = transform;
            Stop();
        }

        private void Update()
        {
            if (m_StillClimb)
            {
                Climbing();
            }
            else if (m_IsGrounded)
                m_RigidBody.velocity = m_CurVelocity * m_Modificater;
            else if (!m_AfterJump)
                m_RigidBody.velocity = Vector2.down * m_MoveSpeed;
        }

        private bool m_StillClimb => m_StartClimb || m_Climbing || m_AfterClimb;

        private void Climbing()
        {
            if(m_StartClimb)
            {
                if (m_Side > 0 ? m_WallRightChecker.IsLayer : m_WallLeftChecker.IsLayer)
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
                    m_AfterClimb = true;
                }
            }

            if (m_AfterClimb)
            {
                if (!m_IsGrounded)
                    m_Transform.position += Vector3.right * m_Side * Time.deltaTime * m_Modificater;
                else
                {
                    m_AfterClimb = false;
                    m_RigidBody.isKinematic = false;
                    Walk();
                }
            }
        }

        public void Climb(float height)
        {
            m_StartClimb = true;
            m_ClimbHeight = height;

            m_AfterJump = false;
        }

        public void TurnAround()
        {
            m_Side *= -1;
            m_CurVelocity.x *= m_Side;

            m_AfterJump = false;
        }

        public void Jump()
        {
            if (m_IsGrounded)
            { 
                m_RigidBody.AddForce(new Vector2(m_JumpForce.x * m_Side, m_JumpForce.y));
                m_AfterJump = true;
            }
        }

        public void Walk()
        {
            m_CurVelocity = new Vector2(m_MoveSpeed * m_Side, m_RigidBody.velocity.y);

            m_AfterJump = false;
        }

        public void Run()
        {
            m_CurVelocity = new Vector2(m_MoveSpeed * m_RunMultiplier * m_Side, m_RigidBody.velocity.y);

            m_AfterJump = false;
        }

        public void Stop()
        {
            m_CurVelocity = Vector2.zero;
        }

        public void AddDamage(float damage)
        {
            m_AfterJump = false;

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
    
