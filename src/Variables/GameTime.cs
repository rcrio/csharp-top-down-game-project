using Raylib_cs;

public class GameTime
{
    public float DeltaTime { get; set; }

    public void Update()
    {
        DeltaTime = Raylib.GetFrameTime();
    }
}