using DG.Tweening;
using UnityEngine;

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

    private int indexSkill1;
    private int indexSkill2;
    private int indexSkill3;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, SkillSelect_Birth, Frame_Birth, Skill1_Birth, Skill2_Birth, Skill3_Birth);
        BindSequences(State.Death, SkillSelect_Death, Frame_Death, Skill1_Death, Skill2_Death, Skill3_Death);

        UpdateUI();
    }

    private void UpdateUI()
    {

    }
}