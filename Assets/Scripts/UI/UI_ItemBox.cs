using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemBox : UI_Popup
{
    #region Inspector
    [SerializeField]
    private Sprite[] sprites;
    #endregion

    private enum Children
    {
        Icon_Item,
        Text_Name,
        Text_Description,
        Value_Damage,
        Value_HP
    }

    private int valueDamage;
    private int valueHP;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        UpdateUI();
    }

    public override void Birth()
    {
        base.Birth();
        DOVirtual.DelayedCall(1.0f, Managers.UI.ClosePopup);
    }

    private void UpdateUI()
    {
        // 공격
        if (Random.Range(0, 2) == 0)
        {
            valueDamage = Random.Range(1, 9);
            valueHP = Random.Range(1, 3);

            Get<Image>((int)Children.Icon_Item).sprite = sprites[0];
            Get<TMP_Text>((int)Children.Text_Name).text = "누군가 잃어버린 탄피";
            Get<TMP_Text>((int)Children.Text_Description).text = "누군가의 군생활이 걱정됩니다.\n이러언~";
        }

        // 방어
        else
        {
            valueDamage = Random.Range(1, 3);
            valueHP = Random.Range(3, 30);

            Get<Image>((int)Children.Icon_Item).sprite = sprites[1];
            Get<TMP_Text>((int)Children.Text_Name).text = "아드로핀 주사기";
            Get<TMP_Text>((int)Children.Text_Description).text = "아드레날린이 솟구칩니다.\n싱글톤 벙글톤.";
        }

        Get<TMP_Text>((int)Children.Value_Damage).text = $"+{valueDamage.ToString()}";
        Get<TMP_Text>((int)Children.Value_HP).text = $"+{valueHP.ToString()}";

        var player = Managers.Game.Player;
        player.StatHandler.Damage += valueDamage;
        player.StatHandler.HP += valueHP;
        player.StatHandler.CurrentHP = player.StatHandler.HP;

        (Managers.UI.CurrentSceneUI as UI_Lobby).UpdateUI();
    }
}