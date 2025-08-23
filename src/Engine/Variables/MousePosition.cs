using System.Numerics;

public class MousePosition
{
    public Vector2 MouseScreen { get; set; }
    public Vector2 MouseWorld { get; set; }

    public MousePosition(Vector2 mouseScreen, Vector2 mouseWorld)
    {
        MouseScreen = mouseScreen;
        MouseWorld = mouseWorld;
    }

    
}