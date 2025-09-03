
public abstract class Scene
{
    public bool RequestPop { get; protected set; }
    public Scene RequestPush { get; protected set; }
    public bool RequestExit { get; protected set; }
    public InputManager InputManager { get; protected set; } 
    public GameTime GameTime { get; protected set; }
    public Scene(InputManager inputManager, GameTime gameTime)
    {
        InputManager = inputManager;
        GameTime = gameTime;

        ResetRequests();
    }

    public abstract void Update();
    public abstract void Draw();
    public abstract void Load();
    public abstract void Unload();
    public void ResetRequests()
    {
        RequestPop = false;
        RequestPush = null;
        RequestExit = false;
    }

}