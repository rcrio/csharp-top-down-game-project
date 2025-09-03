using Raylib_cs;

public class OptionsScene : Scene
{
    // Inherited:
    // InputManager InputManager
    // GameTime GameTime
    // MusicManager MusicManager
    public OptionsScene(InputManager inputManager, GameTime gameTime, MusicManager musicManager) : base(inputManager, gameTime, musicManager)
    { 
        
    }

    public override void Update()
    {
        MusicManager.Update(GameTime.DeltaTime);
        
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