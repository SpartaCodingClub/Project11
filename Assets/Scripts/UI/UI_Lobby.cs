using DG.Tweening;
using TMPro;

public class UI_Lobby : UI_Scene
{
    #region Birth
    private Sequence Frame_Damage_Birth()
    {
        var child = Get((int)Children.Frame_Damage);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosX(-300.0f, 1.0f).From().SetEase(Ease.OutBack).OnComplete(Stand));
    }

    private Sequence Frame_HP_Birth()
    {
        var child = Get((int)Children.Frame_HP);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosX(300.0f, 1.0f).From().SetEase(Ease.OutBack));
    }
    #endregion
    #region Death
    private Sequence Frame_Damage_Death()
    {
        var child = Get((int)Children.Frame_Damage);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosX(-300.0f, 1.0f).SetEase(Ease.InBack).OnComplete(Destroy));
    }

    private Sequence Frame_HP_Death()
    {
        var child = Get((int)Children.Frame_HP);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosX(300.0f, 1.0f).SetEase(Ease.InBack));
    }
    #endregion

    private enum Children
    {
        Frame_Damage,
        Value_Damage,
        Frame_HP,
        Valud_HP
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Frame_Damage_Birth, Frame_HP_Birth);
        BindSequences(State.Death, Frame_Damage_Death, Frame_HP_Death);

        UpdateUI();
    }

    private void UpdateUI()
    {
        PlayerController player = Managers.Game.Player;
        Get<TMP_Text>((int)Children.Value_Damage).text = ((int)player.StatHandler.Damage).ToString();
        Get<TMP_Text>((int)Children.Valud_HP).text = ((int)player.StatHandler.HP).ToString();
    }
}