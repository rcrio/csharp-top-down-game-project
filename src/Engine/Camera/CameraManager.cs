using System.Numerics;
using Raylib_cs;

public class CameraManager
{
    // really annoying, have to set Camera as public without get set or it doesn't work because struct properties
    public Camera2D Camera;
    private Vector2 _offset;
    private Vector2 _target;
    private float _rotation;
    private float _zoom;
    private InputManager _inputManager;


    public CameraManager(Vector2 offset, Vector2 target, InputManager inputManager)
    {
        _offset = offset;   // screen center
        _target = target;   // world position (player)
        _rotation = 0.0f;
        _zoom = 1.0f;
        Camera = new Camera2D(_offset, _target, _rotation, _zoom);
        _inputManager = inputManager;
    }
    public void Update(Vector2 offset, Vector2 target)
    {
        _offset = offset;   // screen center
        _target = target;   // player position
        Camera.Target = _target;
        Camera.Offset = _offset;
        if (_inputManager.ZoomIn()) ZoomIn();
        if (_inputManager.ZoomOut()) ZoomOut();
    }

    public void ZoomOut()
    {
        if (Camera.Zoom > 0.5f) Camera.Zoom -= 0.1f;
    }

    public void ZoomIn()
    {
        if (Camera.Zoom < 3.0f) Camera.Zoom += 0.1f;
    }
}