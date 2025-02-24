using UnityEngine;

public class PlayerController : ObjectController
{
    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.z == 0)
        {
            statHandler.VelocityZ = statHandler.JumpPower;
        }
    }
}
