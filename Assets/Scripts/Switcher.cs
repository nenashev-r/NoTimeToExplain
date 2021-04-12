using UnityEngine;
using UnityEngine.Events;

public class Switcher : MonoBehaviour
{
    [SerializeField] private UnityEvent m_On;
    [SerializeField] private UnityEvent m_Off;
    [SerializeField] private bool m_OnStart;

    private bool m_Switched;

    private void Start()
    {
        m_Switched = m_OnStart;
    }

    public void Switch()
    {
        if (m_Switched)
            m_Off?.Invoke();
        else
            m_On?.Invoke();

        m_Switched = !m_Switched;
    }
}
