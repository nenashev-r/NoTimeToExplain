using System;
using EventSystem;
using UnityEngine;

namespace GameScripts
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private GameEvent m_JumpEvent;
        [SerializeField] private GameEvent m_MoveLeftEvent;
        [SerializeField] private GameEvent m_MoveRightEvent;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_JumpEvent.Invoke();
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                m_MoveLeftEvent.Invoke();
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                m_MoveRightEvent.Invoke();
            }
        }
    }
}