using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowMultiplier = 0.7f; // 속도를 줄이는 비율
    private float originalSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StatHandler statHandler = other.GetComponent<StatHandler>();
        if (statHandler != null)
        {
            originalSpeed = statHandler.MoveSpeed;
            statHandler.MoveSpeed *= slowMultiplier; // 속도 감소
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StatHandler statHandler = other.GetComponent<StatHandler>();
        if (statHandler != null)
        {
            statHandler.MoveSpeed = originalSpeed; // 원래 속도로 복구
        }
    }
}