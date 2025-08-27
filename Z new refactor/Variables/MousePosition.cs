using System.Numerics;
using Raylib_cs;

public class MousePosition
{
    public Vector2 MouseScreen { get; set; }
    public Vector2 MouseWorld { get; set; }
    private Camera2D _camera;

    public MousePosition(Camera2D camera)
    {
        Update(camera);
    }

    public void Update(Camera2D camera)
    {
        MouseScreen = Raylib.GetMousePosition();
        MouseWorld = Raylib.GetScreenToWorld2D(MouseScreen, camera);
    }

    
}