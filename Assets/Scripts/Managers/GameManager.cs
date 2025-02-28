using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerController Player { get; set; }

    public readonly Queue<int> MonsterCount = new();

    private int currentMonsterCount;
    private Scene_Game gameScene;

    public void Initialize()
    {
#if DEBUG
        Application.runInBackground = true;
        SRDebug.Init();
#endif

        Application.targetFrameRate = 60;
        DOTween.SetTweensCapacity(200, 2048);
    }

    public void MonsterOnDead()
    {
        if (gameScene == null)
        {
            gameScene = Managers.Scene.GetCurrentScene<Scene_Game>();
        }

        if (currentMonsterCount == 0)
        {
            // 다음 스테이지
            SetNextStage();
        }

        if (--currentMonsterCount > 0)
        {
            return;
        }

        DOVirtual.DelayedCall(1.0f, () =>
        {
            Managers.UI.Show<UI_SkillSelect>().OnDestoryed += () =>
            {
                gameScene.GenerateMap();
                DOVirtual.DelayedCall(2.0f, () => gameScene.CurrentSpawner.CameraCollider.simulated = true);
            };
        });
    }

    private void SetNextStage()
    {
        gameScene.NextSpawner.CameraCollider.simulated = false;
        currentMonsterCount = MonsterCount.Dequeue();
    }
}