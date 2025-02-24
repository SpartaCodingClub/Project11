using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas), typeof(CanvasScaler), typeof(CanvasGroup))]
public abstract class UI_Base : BaseController
{
    protected CanvasGroup canvasGroup;

    private readonly List<RectTransform> children = new();

    protected RectTransform Get(int index) => children[index];
    protected T Get<T>(int index) where T : Component => Get(index).GetComponent<T>();

    protected override void Initialize()
    {
        base.Initialize();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public override void Birth()
    {
        base.Birth();
        canvasGroup.interactable = false;
    }

    public override void Stand()
    {
        base.Stand();
        canvasGroup.interactable = true;
    }

    public override void Death()
    {
        base.Death();
        canvasGroup.interactable = false;
    }

    public override void Destroy()
    {
        base.Destroy();
        canvasGroup.interactable = false;
    }

    protected void BindChildren(Type enumType)
    {
        var names = Enum.GetNames(enumType);
        foreach (var name in names)
        {
            RectTransform child = gameObject.FindComponent<RectTransform>(name);
            children.Add(child);
        }
    }

    protected void BindEvent(int index, UnityAction @event)
    {
        Button button = Get<Button>(index);
        if (button == null)
        {
            Debug.LogWarning($"Failed to BindEvent({index})\nFrom: {gameObject.name}");
            return;
        }

        button.onClick.AddListener(@event);
    }
}