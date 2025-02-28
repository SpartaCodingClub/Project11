using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelect : UI_SubItem
{
    #region Birth
    private Sequence SkillSelect_Birth()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(1.0f, 0.5f).From(0.0f));
    }

    private Sequence Frame_Birth()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(200.0f, 0.5f).From());
    }

    private Sequence Skill1_Birth()
    {
        var child = Get((int)Children.Skill1);

        return Utility.RecyclableSequence()
            .Append(child.DORotate(90.0f * Vector3.up, 0.2f).From().SetDelay(0.5f));
    }

    private Sequence Skill2_Birth()
    {
        var child = Get((int)Children.Skill2);

        return Utility.RecyclableSequence()
            .Append(child.DORotate(90.0f * Vector3.up, 0.2f).From().SetDelay(0.7f));
    }

    private Sequence Skill3_Birth()
    {
        var child = Get((int)Children.Skill3);

        return Utility.RecyclableSequence()
            .Append(child.DORotate(90.0f * Vector3.up, 0.2f).From().SetDelay(0.9f).OnComplete(Stand));
    }
    #endregion
    #region Death
    private Sequence SkillSelect_Death()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(0.0f, 0.5f));
    }

    private Sequence Frame_Death()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosY(200.0f, 0.5f));
    }

    private Sequence Skill1_Death()
    {
        var child = Get((int)Children.Skill1);

        return Utility.RecyclableSequence()
            .Append(child.DORotate(90.0f * Vector3.up, 0.2f));
    }

    private Sequence Skill2_Death()
    {
        var child = Get((int)Children.Skill2);

        return Utility.RecyclableSequence()
            .Append(child.DORotate(90.0f * Vector3.up, 0.2f));
    }

    private Sequence Skill3_Death()
    {
        var child = Get((int)Children.Skill3);

        return Utility.RecyclableSequence()
            .Append(child.DORotate(90.0f * Vector3.up, 0.2f).OnComplete(Destroy));
    }
    #endregion

    private enum Children
    {
        Frame,

        Skill1,
        SkillName1,
        Icon1,

        Skill2,
        SkillName2,
        Icon2,

        Skill3,
        SkillName3,
        Icon3
    }

    private int index_Skill1;
    private int index_Skill2;
    private int index_Skill3;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, SkillSelect_Birth, Frame_Birth, Skill1_Birth, Skill2_Birth, Skill3_Birth);
        BindSequences(State.Death, SkillSelect_Death, Frame_Death, Skill1_Death, Skill2_Death, Skill3_Death);

        UpdateUI();
    }

    public override void Birth()
    {
        base.Birth();
        Managers.Audio.Play(Clip.SoundFX_SkillSelect);
    }

    public override void Death()
    {
        base.Death();
        Managers.Audio.Play(Clip.SoundFX_SkillSelected);
    }

    private void UpdateUI()
    {
        Get<Button>((int)Children.Skill1).onClick.RemoveAllListeners();
        Get<Button>((int)Children.Skill2).onClick.RemoveAllListeners();
        Get<Button>((int)Children.Skill3).onClick.RemoveAllListeners();

        index_Skill1 = Random.Range(0, 3);
        Get<TMP_Text>((int)Children.SkillName1).text = Managers.Skill.GetSkillData(index_Skill1).SkillName;

        index_Skill2 = Random.Range(3, 5);
        Get<TMP_Text>((int)Children.SkillName2).text = Managers.Skill.GetSkillData(index_Skill2).SkillName;

        index_Skill3 = Random.Range(5, 7);
        Get<TMP_Text>((int)Children.SkillName3).text = Managers.Skill.GetSkillData(index_Skill3).SkillName;

        Get<Button>((int)Children.Skill1).onClick.AddListener(() =>
        {
            Managers.Skill.SetSkillData(index_Skill1);
            Death();
        });

        Get<Button>((int)Children.Skill2).onClick.AddListener(() =>
        {
            Managers.Skill.SetSkillData(index_Skill2);
            Death();
        });

        Get<Button>((int)Children.Skill3).onClick.AddListener(() =>
        {
            Managers.Skill.SetSkillData(index_Skill3);
            Death();
        });
    }
}