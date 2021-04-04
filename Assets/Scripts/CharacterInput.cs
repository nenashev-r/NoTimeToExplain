using EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private GameEvent m_JumpEvent;
        [SerializeField] private GameEvent m_MoveLeftEvent;
        [SerializeField] private GameEvent m_MoveRightEvent;

        [Space]
        [SerializeField] private Button m_JumpButton;
        [SerializeField] private PressButton m_MoveLeftButton;
        [SerializeField] private PressButton m_MoveRightButton;


        private void OnEnable()
        { 
            m_JumpButton.onClick.AddListener(Jump);
            m_MoveLeftButton.OnPressed.AddListener(MoveLeft);
            m_MoveRightButton.OnPressed.AddListener(MoveRight);
        }
        
        private void OnDisable()
        { 
            m_JumpButton.onClick.RemoveListener(Jump);
            m_MoveLeftButton.OnPressed.RemoveListener(MoveLeft);
            m_MoveRightButton.OnPressed.RemoveListener(MoveRight);
        }

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

        private void Jump()
        {
            m_JumpEvent.Invoke();
        }
        
        private void MoveLeft()
        {
            m_MoveLeftEvent.Invoke();
        }

        private void MoveRight()
        {
            m_MoveRightEvent.Invoke();
        }
    }
}