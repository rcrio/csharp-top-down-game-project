using System.Numerics;
using Raylib_cs;

public class MousePosition
{
    public Vector2 MouseScreen { get; set; }
    public Vector2 MouseWorld { get; set; }
    public CameraManager CameraManager { get; set; }

    public MousePosition(CameraManager cameraManager)
    {
        CameraManager = cameraManager;
        Update(CameraManager);
    }

    public void Update(CameraManager cameraManager)
    {
        MouseScreen = Raylib.GetMousePosition();
        MouseWorld = Raylib.GetScreenToWorld2D(MouseScreen, cameraManager.Camera);
    }

    
}