public abstract class Scene
{
    public bool RequestPop { get; protected set; } = false;
    public Scene RequestPush { get; protected set; }
    public bool RequestExit { get; protected set; } = false;
    public InputManager InputManager { get; protected set; } 
    public GameTime GameTime { get; protected set; }
    public MusicManager MusicManager { get; protected set; }

    public abstract void Update();
    public abstract void Draw();

    public abstract void Unload();

    public void ResetRequests()
    {
        RequestPop = false;
        RequestPush = null;
        RequestExit = false;
    }

}