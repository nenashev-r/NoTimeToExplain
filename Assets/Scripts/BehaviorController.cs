using GameScripts.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class BehaviorController : MonoBehaviour, IDamageable, IGroundModificate
    {
        private CharController m_CharacterController;

        private float m_SpeedModificater = 1;

        private void Start()
        {
            m_CharacterController = GetComponent<CharController>();
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

