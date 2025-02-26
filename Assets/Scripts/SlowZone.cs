using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowMultiplier = 0.7f; // 속도를 줄이는 비율
    private float originalSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StatHandler statHandler = other.GetComponent<StatHandler>();
        if (statHandler == null)
        {
            return;
        }

        originalSpeed = statHandler.MoveSpeed;
        statHandler.MoveSpeed *= slowMultiplier; // 속도 감소

        AnimationHandler animationHandler = other.gameObject.FindComponent<AnimationHandler>(Define.MainRenderer);
        if (animationHandler != null)
        {
            animationHandler.HasSlow = true; // 슬로우 상태 설정
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StatHandler statHandler = other.GetComponent<StatHandler>();
        if (statHandler == null)
        {
            statHandler.MoveSpeed = originalSpeed; // 원래 속도로 복구
            return;
        }

        AnimationHandler animationHandler = other.gameObject.FindComponent<AnimationHandler>(Define.MainRenderer);
        if (animationHandler != null)
        {
            animationHandler.HasSlow = false; // 슬로우 상태 해제
        }
    }
}