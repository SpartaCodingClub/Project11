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
        if (IsDead)
        {
            return;
        }

        if (collider.CompareTag(Define.Player))
        {
            Death();

            Managers.Audio.Play(Clip.SoundFX_GetItem);
            Managers.UI.Show<UI_ItemBox>();
        }
    }
}