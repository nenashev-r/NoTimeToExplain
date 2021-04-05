using UnityEngine;

public class GroundObstacle : MonoBehaviour
{
    [SerializeField] private float m_SpeedModificater;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform?.GetComponent<IGroundModificate>().ChangeSpeed(m_SpeedModificater);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform?.GetComponent<IGroundModificate>().ChangeSpeed(1);
    }
}
