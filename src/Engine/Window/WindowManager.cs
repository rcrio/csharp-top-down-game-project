using Raylib_cs;

// Handles window creation and changes to the window settings.
public class WindowManager
{
    public int ScreenWidth { get; private set; }
    public int ScreenHeight { get; private set; }
    public int FramesPerSecond { get; private set; }

    // Constructor
    public WindowManager(int screenWidth, int screenHeight, int framesPerSecond)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        FramesPerSecond = framesPerSecond;
    }

    // Create and initialize the window
    public void Init()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.MaximizedWindow);
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Verdania");
        Raylib.SetExitKey(0);
        Raylib.SetTargetFPS(FramesPerSecond);
    }

    // Optional setters if you want to change FPS or screen size at runtime
    public void SetFPS(int fps)
    {
        FramesPerSecond = fps;
        Raylib.SetTargetFPS(FramesPerSecond);
    }

    public void SetScreenSize(int width, int height)
    {
        ScreenWidth = width;
        ScreenHeight = height;
        Raylib.SetWindowSize(ScreenWidth, ScreenHeight);
    }
}
