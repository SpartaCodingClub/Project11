using DG.Tweening;
using UnityEngine;

public class GameManager
{
    public void Initialize()
    {
#if DEBUG
        Application.runInBackground = true;
        SRDebug.Init();
#endif

        Application.targetFrameRate = 60;
        DOTween.SetTweensCapacity(200, 125);
    }
}