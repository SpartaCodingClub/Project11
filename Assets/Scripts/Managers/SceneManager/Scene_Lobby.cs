using DG.Tweening;
using UnityEngine;

public class Scene_Lobby : Scene_Base
{
    private UI_Tutorial tutorial;

    private int tutorialIndex;

    protected override void Initialize()
    {
        base.Initialize();

        Managers.Resource.Instantiate<PlayerController>(null, Vector2.zero);
        Managers.UI.Show<UI_PopupBackground>().Death();

        NextTutorial(4.0f);
    }

    private void NextTutorial(float delay)
    {
        string[] messages = GetTutorialMessages();
        if (messages == null)
        {
            Managers.UI.Show<UI_GameStart>();
            return;
        }

        DOVirtual.DelayedCall(delay, () =>
        {
            tutorial = Managers.UI.Show<UI_Tutorial>();
            tutorial.SetMessage(messages);
            tutorial.OnDestoryed += () => NextTutorial(10.0f);
        });
    }

    private string[] GetTutorialMessages()
    {
        switch (tutorialIndex++)
        {
            case 0:
                return Define.Tutorial_Move;
            case 1:
                return Define.Tutorial_Jump;
            case 2:
                return Define.Tutorial_Explore;
        }

        return null;
    }
}