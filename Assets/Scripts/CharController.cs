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
        [SerializeField] private float m_FallSpeed;

        [Space]
        [SerializeField] private List<LayerRayChecker> m_GroundCheckers;
        [SerializeField] private List<LayerRayChecker> m_WallCheckers;

        [Space]
        [SerializeField] private Transform m_StartPosition;

        private Transform m_Transform;
        private Vector2 m_CurVelocity;

        private bool m_IsGrounded => m_GroundCheckers.Any(o => o.IsLayer);
        [Header("check")]
        [SerializeField] bool Grounded;
        [SerializeField] bool Wall;
        public float Modificater { get; set; } = 1;

        private int m_Side = 1;

        private bool m_StartClimb;
        private bool m_Climbing;
        private bool m_AfterClimb;
        private float m_ClimbHeight;

        private bool m_AfterJump;

        private void Start()
        {
            m_Transform = transform;

            if (m_StartPosition != null)
                m_Transform.position = m_StartPosition.position;

            Stop();
        }

        private void FixedUpdate()
        {
            Grounded = m_IsGrounded;
            Wall = m_WallCheckers.Any(o => o.IsLayer);
            if (m_StillClimb)
            {
                Climbing();
            }
            else if (m_IsGrounded)
                m_RigidBody.velocity = m_CurVelocity * Modificater;
            else if (!m_AfterJump)
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, m_RigidBody.velocity.y * m_FallSpeed);
        }

        private bool m_StillClimb => m_StartClimb || m_Climbing || m_AfterClimb;

        private void Climbing()
        {
            if(m_StartClimb)
            {                
                if (m_WallCheckers.Any(o => o.IsLayer))
                {
                    m_StartClimb = false;
                    m_Climbing = true;
                    m_AfterClimb = false;


                    m_RigidBody.isKinematic = true;
                    m_RigidBody.velocity = Vector2.zero;
                }
                else
                    m_RigidBody.velocity = m_CurVelocity * Modificater;
            }            
            
            if(m_Climbing)
            {
                m_Transform.position += Vector3.up * Time.deltaTime * m_ClimbSpeed * Modificater;

                if (m_WallCheckers.All(o => !o.IsLayer))
                {
                    m_Climbing = false;
                    m_AfterClimb = true;
                }
            }

            if (m_AfterClimb)
            {
                if (!m_IsGrounded)
                    m_Transform.position += Vector3.right * m_Side * Time.deltaTime * Modificater;
                else
                {
                    m_AfterClimb = false;
                    m_RigidBody.isKinematic = false;
                }
            }
        }

        public void Climb()
        {
            Walk();

            m_StartClimb = true;
            m_AfterJump = false;
        }

        public void TurnAround()
        {
            m_Side *= -1;
            m_CurVelocity.x *= -1;

            m_WallCheckers.ForEach(o => o.Direction *= -1);
            m_Transform.localScale = new Vector3(-m_Transform.localScale.x, m_Transform.localScale.y, m_Transform.localScale.z);

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
    
