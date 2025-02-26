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

        //NextTutorial(4.0f);
        Managers.UI.Show<UI_GameStart>();
    }

    private void NextTutorial(float delay)
    {
        string[] messages = GetTutorialMessages();
        if (messages == null)
        {
            DOVirtual.DelayedCall(1.0f, () => Managers.UI.Show<UI_GameStart>());
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
        return tutorialIndex++ switch
        {
            0 => Define.Tutorial_Move,
            1 => Define.Tutorial_Jump,
            2 => Define.Tutorial_Explore,
            _ => null
        };
    }
}