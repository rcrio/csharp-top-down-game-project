using Raylib_cs;

public class OptionsScene : Scene
{
    // InputManager and GameTime inherited from Scene
    public OptionsScene(InputManager inputManager, GameTime gameTime)
    {
        InputManager = inputManager;
        GameTime = gameTime;
    }

    public override void Update()
    {
        if (InputManager.Return())
        {
            RequestPop = true;
        }
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);
        Raylib.DrawText("Nothing here yet...", 50, 20, 20, Color.RayWhite);
    }

    public override void Unload()
    {
        
    }

    
}