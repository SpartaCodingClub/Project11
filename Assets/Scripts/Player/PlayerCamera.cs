using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;  // 따라갈 플레이어
    public Vector2 minBounds; // 맵의 최소 X, Y 값 (맵의 왼쪽 하단)
    public Vector2 maxBounds; // 맵의 최대 X, Y 값 (맵의 오른쪽 상단)

    void LateUpdate()
    {
        if (player != null)
        {
            // 카메라의 새로운 위치 계산
            float clampedX = Mathf.Clamp(player.position.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(player.position.y, minBounds.y, maxBounds.y);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
