using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnPressed;

    private void Update()
    {
        if(m_IsPointerDown)
            OnPressed.Invoke();
    }

    private bool m_IsPointerDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        m_IsPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_IsPointerDown = false;
    }
}
