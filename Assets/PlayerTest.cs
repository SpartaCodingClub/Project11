using UnityEngine;

public class PlayerTest : ObjectController
{
    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.z == 0)
        {
            statHandler.VelocityZ = statHandler.JumpPower;
        }
    }
}