using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameScripts
{
    public class Clickable : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent m_OnClick;

        public UnityEvent OnClick => m_OnClick;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("click");
            m_OnClick?.Invoke();
        }
    }
}