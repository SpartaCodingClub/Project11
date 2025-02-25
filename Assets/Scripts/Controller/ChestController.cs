using UnityEngine;

public class ChestController : ObjectController
{

    protected override void Initialize()
    {
        base.Initialize();

        lookDirection = Vector3.down;
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
