using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial : UI_SubItem
{
    #region Birth
    private Sequence Frame_Birth()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOScale(1.0f, 0.2f).From(0.0f))
            .AppendCallback(() => StartCoroutine(StartMessage()));
    }

    private Sequence Text_Birth()
    {
        return Utility.RecyclableSequence();
    }

    private Sequence Character_Birth()
    {
        var child = Get((int)Children.Character);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosX(700.0f, 0.5f).From());
    }
    #endregion
    #region Stand
    private Sequence Button_Stand()
    {
        var graphic = Get<Graphic>((int)Children.Button);

        return Utility.RecyclableSequence()
            .Append(graphic.DOFade(1.0f, 1.0f))
            .Append(graphic.DOFade(0.0f, 1.0f));
    }
    #endregion
    #region Death
    private Sequence Tutorial_Death()
    {
        return Utility.RecyclableSequence()
            .Append(canvasGroup.DOFade(0.0f, 0.5f).OnComplete(Destroy));
    }

    private Sequence Character_Death()
    {
        var child = Get((int)Children.Character);

        return Utility.RecyclableSequence()
            .Append(child.DOAnchorPosX(700.0f, 0.5f));
    }
    #endregion
    #region Event
    private void Button_Frame()
    {
        if (messages.Count == 0)
        {
            Death();
            return;
        }

        ClearText();
        StartCoroutine(StartMessage());
    }
    #endregion

    private enum Children
    {
        Frame,
        Button,
        Text,
        Character
    }

    private TMP_Text message;

    private readonly WaitForSeconds shortInterval = new(0.04f);
    private readonly WaitForSeconds longInterval = new(0.4f);
    private readonly Queue<string> messages = new();

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequences(State.Birth, Text_Birth, Character_Birth);
        BindSequences(State.Birth, Frame_Birth);
        BindSequences(State.Stand, Button_Stand);
        BindSequences(State.Death, Tutorial_Death, Character_Death);

        BindEvent((int)Children.Frame, Button_Frame);

        message = Get<TMP_Text>((int)Children.Text);
        ClearText();
    }

    public void ClearText()
    {
        canvasGroup.interactable = false;
        message.text = string.Empty;
        sequenceHandler.Stand.Pause();

        var grahpic = Get<Graphic>((int)Children.Button);
        Color newColor = grahpic.color;
        newColor.a = 0.0f;
        grahpic.color = newColor;
    }

    public void SetMessage(params string[] messages)
    {
        foreach (var message in messages)
        {
            this.messages.Enqueue(message);
        }
    }

    private IEnumerator StartMessage()
    {
        char[] charArray = messages.Dequeue().ToCharArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            message.text += charArray[i];
            if (charArray[i] == '\n')
            {
                yield return longInterval;
            }
            else
            {
                Managers.Audio.Play(Clip.SoundFX_TypingSound);
                yield return shortInterval;
            }
        }

        Stand();
    }
}