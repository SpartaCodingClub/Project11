public class Scene_Title : Scene_Base
{
    public RainController RainParticleSystem;

    protected override void Initialize()
    {
        base.Initialize();

        Managers.Audio.Play(Clip.Ambient_Rain);
        Managers.Audio.Play(Clip.Music_Title);
        Managers.UI.Show<UI_Title>();
    }
}