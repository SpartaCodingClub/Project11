using UnityEngine;

public class ChestController : BaseController
{
    private AnimationHandler animationHandler;
    private Rigidbody2D _rigidbody;


    protected override void Initialize()
    {
        base.Initialize();

        Stand();
    }
    public override void Stand()
    {
        base.Stand();

        animationHandler.Stand(Vector2.down);
    }

    public override void Death()
    {
        base.Death();

        animationHandler.Death(Vector2.down);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log(collider.name);
            Death();
        }
    }

}
