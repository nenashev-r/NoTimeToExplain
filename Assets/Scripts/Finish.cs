using EventSystem;
using UnityEngine;

namespace GameScripts
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private GameEvent m_finalEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            m_finalEvent?.Invoke();
        }
    }
}

