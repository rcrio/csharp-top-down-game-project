using Raylib_cs;

public class SceneManager
{
    private Stack<Scene> _scenes = new Stack<Scene>();
    private Scene? _nextScene;
    private bool _popPending = false;

    // Fade variables
    private float _fadeAlpha = 1f;
    private float _fadeSpeed = 0.5f;
    private bool _fadingIn = true;

    //private MusicManager _music;
    private GameTime _time;

    public SceneManager(InputManager input, GameTime time)
    {
        _time = time;
        //_music = new MusicManager();
        var mainMenuScene = new MainMenuScene(input, time/*_music*/);
        mainMenuScene.Load();
        _scenes.Push(mainMenuScene);
    }

    public void Push(Scene scene)
    {
        _nextScene = scene;
        _fadingIn = false;
        _fadeAlpha = 0f;
    }

    public void Pop()
    {
        _popPending = true;
        _fadingIn = false;
        _fadeAlpha = 0f;
    }

    public void Update()
    {
        if (_scenes.Count == 0) return;

        float delta = _time.DeltaTime;

        if (!_fadingIn)
        {
            _fadeAlpha += _fadeSpeed * delta;
            if (_fadeAlpha >= 1f)
            {
                _fadeAlpha = 1f;

                if (_popPending)
                {
                    _scenes.Peek().Unload();
                    _scenes.Pop();
                    _popPending = false;
                }

                if (_nextScene != null)
                {
                    _nextScene.Load();
                    _scenes.Push(_nextScene);
                    _nextScene = null;
                }

                _fadingIn = true;
            }
            else return;
        }
        else if (_fadeAlpha > 0f)
        {
            _fadeAlpha -= _fadeSpeed * delta;
            if (_fadeAlpha < 0f) _fadeAlpha = 0f;
        }

        var active = _scenes.Peek();
        active.Update();
        //_music.Update(delta);

        if (active.RequestExit) _scenes.Clear();
        if (active.RequestPop) Pop();
        if (active.RequestPush != null) Push(active.RequestPush);

        active.ResetRequests();
    }

    public void Draw()
    {
        if (_scenes.Count == 0) return;

        _scenes.Peek().Draw();

        if (_fadeAlpha > 0f)
        {
            Raylib.DrawRectangle(
                0, 0,
                Raylib.GetScreenWidth(),
                Raylib.GetScreenHeight(),
                new Color((byte)0, (byte)0, (byte)0, (byte)(_fadeAlpha * 255))
            );
        }
    }

    public bool ShouldClose() => _scenes.Count == 0;
}
