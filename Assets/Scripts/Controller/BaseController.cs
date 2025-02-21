using DG.Tweening;
using System;
using UnityEditor;
using UnityEngine;
using VInspector;

public enum State
{
    Destroyed,
    Birth,
    Stand,
    Death
}

public abstract class BaseController : MonoBehaviour
{
    #region Inspector  
#if UNITY_EDITOR
    [ShowInInspector]
    public State CurrentState
    {
        get
        {
            if (EditorApplication.isPlaying == false)
            {
                return State.Destroyed;
            }

            EditorUtility.SetDirty(this);
            return state;
        }
    }
#endif
    #endregion

    public bool IsDead { get { return state == State.Death || state == State.Destroyed; } }

    public event Action OnBirth;
    public event Action OnStand;
    public event Action OnDeath;
    public event Action OnDestoryed;

    private State state;

    private readonly SequenceHandler sequenceHandler = new();

    private void Awake() => Initialize();
    private void OnDestroy() => Deinitialize();
    protected void BindSequences(State type, params Func<Sequence>[] sequences) => sequenceHandler.Bind(type, sequences);

    protected virtual void Initialize()
    {
        sequenceHandler.Initialize();
    }

    protected virtual void Deinitialize()
    {
        sequenceHandler.Deinitialize();
    }

    public virtual void Clear()
    {
        OnBirth = null;
        OnStand = null;
        OnDeath = null;
        OnDestoryed = null;
    }

    public virtual void Birth()
    {
        state = State.Birth;

        sequenceHandler.Birth.Restart();

        OnBirth?.Invoke();
    }

    public virtual void Stand()
    {
        state = State.Stand;

        sequenceHandler.Stand.Restart();

        OnStand?.Invoke();
    }

    public virtual void Death()
    {
        state = State.Death;

        sequenceHandler.Stand.Pause();
        sequenceHandler.Death.Restart();

        OnDeath?.Invoke();
    }

    public virtual void Destroy()
    {
        state = State.Destroyed;

        sequenceHandler.Stand.Pause();

        OnDestoryed?.Invoke();
        Managers.Resource.Destroy(gameObject);
    }
}