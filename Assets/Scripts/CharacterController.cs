using EventSystem;
using UnityEngine;

namespace GameScripts
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameEvent m_DiedEvent;
        [SerializeField] private GameEvent m_SpawnedEvent;

        public void Jump()
        {
            Debug.Log("Character Jump");
        }

        public void MoveLeft()
        {
            Debug.Log("Character MoveLeft");
        }

        public void MoveRight()
        {
            Debug.Log("Character MoveRight");
        }
    }
}
    
