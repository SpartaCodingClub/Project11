using UnityEngine;
using VInspector;

[ExecuteAlways]
public class BaseObject : BaseController
{
    #region Inspector
    [SerializeField]
    private bool randomableObject = true;

    [HideIf("randomableObject")]
    [SerializeField, Range(0.0f, 1.0f)]
    private float normalizedTime;
    #endregion

    private Animator animator;
    private float lastNormalizedTime;

    protected override void Initialize()
    {
        base.Initialize();
        animator = GetComponent<Animator>();

        if (randomableObject)
        {
            normalizedTime = Random.Range(0.0f, 1.0f);
        }

        animator.Play(Define.Stand, 0, normalizedTime);
    }

    private void Update()
    {
        if (lastNormalizedTime == normalizedTime)
        {
            return;
        }
        lastNormalizedTime = normalizedTime;

        animator.Play(Define.Stand, 0, normalizedTime);
        if (Application.isPlaying == false)
        {
            animator.Update(0.0f);
        }
    }
}