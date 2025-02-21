using DG.Tweening;
using UnityEngine;

public class PlayerTest : ObjectController
{
    protected override void Initialize()
    {
        base.Initialize();

        DOVirtual.DelayedCall(2.0f, Death);
    }

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

    public override void Destroy()
    {
        base.Destroy();
        Debug.Log("TEST!");
    }
}