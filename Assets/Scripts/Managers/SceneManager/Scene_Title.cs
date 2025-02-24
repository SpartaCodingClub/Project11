using UnityEngine;

public class Scene_Title : Scene_Base
{
    #region Inspector
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private RainController rain;
    #endregion

    protected override void Initialize()
    {
        base.Initialize();

        Managers.Audio.Play(Clip.Ambient_Rain);
        Managers.Audio.Play(Clip.Music_Title);
        Managers.UI.Show<UI_Title>();
    }

    public void Clear()
    {
        player.SetActive(false);
        rain.Stop();
    }
}