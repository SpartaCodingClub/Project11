using DG.Tweening;
using UnityEngine;

public class GameManager
{
    public PlayerController Player { get; set; }

    public int MonsterCount { get; set; } = 0;

    public void Initialize()
    {
#if DEBUG
        Application.runInBackground = true;
        SRDebug.Init();
#endif

        Application.targetFrameRate = 60;
        DOTween.SetTweensCapacity(200, 2048);
    }
}