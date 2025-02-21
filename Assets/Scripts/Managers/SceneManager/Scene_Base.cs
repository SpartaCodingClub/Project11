using UnityEngine;

public abstract class Scene_Base : MonoBehaviour
{
    private void Awake() => Initialize();

    protected virtual void Initialize()
    {
        if (Managers.Instance != null)
        {
            return;
        }

        new GameObject(nameof(Managers), typeof(Managers));
    }
}