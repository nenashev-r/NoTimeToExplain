using UnityEngine;

public class LayerRayChecker : MonoBehaviour
{
    [SerializeField] private LayerMask m_Layer;
    [SerializeField] private float m_RayDistance = 0.02f;
    [SerializeField] private Vector2 m_RayDirection;

    public bool IsLayer { get; private set; } = true;

    private void Update()
    {
        var hit = Physics2D.Raycast(transform.position, m_RayDirection, m_RayDistance, m_Layer);

        if (hit.collider != null)
            IsLayer = true;
        else
            IsLayer = false;
    }
}
