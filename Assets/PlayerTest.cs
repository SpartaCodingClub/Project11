using UnityEngine;

public class PlayerTest : ObjectController
{
    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontal, vertical).normalized;
        Debug.Log(direction == Vector2.zero);

        if (direction == Vector2.zero) return;

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.z == 0)
        {
            statHandler.VelocityZ = statHandler.JumpPower;
        }
    }
}