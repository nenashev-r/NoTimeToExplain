using EventSystem;
using GameScripts.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class BehaviorController : MonoBehaviour, IDamageable, IGroundModificate
    {
        [SerializeField] private GameEvent m_DefaultEvent;

        private Transform m_Transform;

        private CharController m_CharacterController;

        private float m_SpeedModificater = 1;

        private Queue<GameEvent> m_Events;
        private GameEvent m_CurEvent;

        private float m_LastPosX;

        private void Start()
        {
            m_Transform = transform;
            m_CharacterController = GetComponent<CharController>();

            m_DefaultEvent?.Invoke();
        }

        private void Update()
        {
            if (m_Events == null || m_Events.Count == 0)
                return;

            if (m_CurEvent == null || m_Transform.position.x - m_LastPosX >= m_CurEvent.Distance)
                ActivateNextEvent();
        }

        private GameEvent TakeAnotherAction()
        {
            if (m_Events != null && m_Events.Count > 0)
                return m_Events.Dequeue();

            return null;
        }

        private void ActivateNextEvent()
        {
            m_LastPosX = m_Transform.position.x;

            m_CurEvent = TakeAnotherAction();

            if (m_CurEvent == null)
                m_DefaultEvent?.Invoke();
            else
                m_CurEvent.Invoke();
        }

        public void SetEventsQueue(Queue<GameEvent> events)
        {
            m_Events = events;

            ActivateNextEvent();
        }

        public void Jump()
        {
            if (m_CharacterController == null)
                return;

            m_CharacterController.Jump();
        }

        public void Run()
        {
            if (m_CharacterController == null)
                return;

            m_CharacterController.Run(m_SpeedModificater);
        }

        public void Walk()
        {
            if (m_CharacterController == null)
                return;

            m_CharacterController.Walk(m_SpeedModificater);
        }
        public void Stop()
        {
            if (m_CharacterController == null)
                return;

            m_CharacterController.Stop();
        }

        public void AddDamage(float damage)
        {
            if (m_CharacterController == null)
                return;

            m_CharacterController.AddDamage(damage);
        }

        public void ChangeSpeed(float modificater)
        {
            m_SpeedModificater = modificater;

            if (m_CharacterController == null)
                return;
            m_CharacterController.Modificater = m_SpeedModificater;
        }
    }
}

