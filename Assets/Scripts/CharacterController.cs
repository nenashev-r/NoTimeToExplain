using EventSystem;
using GameScripts.GlobalVariables;
using UnityEngine;

namespace GameScripts
{
    public class CharacterController : MonoBehaviour
    {
        // Global variables
        [SerializeField] private GlobalFloat m_HitPoints;
        
        // Game events
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
    
