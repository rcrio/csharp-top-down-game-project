using Raylib_cs;

public class SceneManager
{
    private Stack<Scene> _scenes = new Stack<Scene>();
    private Scene _nextScene = null!;
    private bool _popPending = false;

    // Screen fade
    private float _fadeAlpha = 1f;
    private float _fadeSpeed = 0.5f;
    private bool _fadingIn = true;

    // Music
    private Music _currentMusic;
    private bool _musicLoaded = false;
    private float _musicVolume = 1f;
    private float _musicFadeSpeed = 0.5f; // per second
    private float _targetMusicVolume = 1f;

    private GameTime _time;

    public SceneManager(InputManager input, GameTime time)
    {
        _time = time;

        // Load initial scene
        var mainMenu = new MainMenuScene(input, time);
        mainMenu.Load();
        _scenes.Push(mainMenu);

        // Start music if present
        _currentMusic = mainMenu.Music;
        _musicLoaded = true; // assume loaded from AssetManager
        _musicVolume = 0f;
        _targetMusicVolume = 1f;
        Raylib.PlayMusicStream(_currentMusic);
        Raylib.SetMusicVolume(_currentMusic, _musicVolume);
    }

    public void Push(Scene scene)
    {
        _nextScene = scene;
        _fadingIn = false;
        _fadeAlpha = 0f;

        // Fade out current music
        if (_musicLoaded) _targetMusicVolume = 0f;
    }

    public void Pop()
    {
        _popPending = true;
        _fadingIn = false;
        _fadeAlpha = 0f;

        if (_musicLoaded) _targetMusicVolume = 0f;
    }

    public void Update()
    {
        if (_scenes.Count == 0) return;

        float delta = _time.DeltaTime;

        // --- Screen fade logic ---
        if (!_fadingIn)
        {
            _fadeAlpha += _fadeSpeed * delta;
            if (_fadeAlpha >= 1f)
            {
                _fadeAlpha = 1f;

                // 1️⃣ Handle pop first
                if (_popPending && _scenes.Count > 0)
                {
                    _scenes.Peek().Unload();
                    _scenes.Pop();
                    _popPending = false;
                }

                // 2️⃣ Handle push / replace
                if (_nextScene != null)
                {
                    // Optional: prevent duplicate top scene
                    if (_scenes.Count == 0 || _scenes.Peek().GetType() != _nextScene.GetType())
                    {
                        _nextScene.Load();
                        _scenes.Push(_nextScene);

                        // Start new music if any
                        _currentMusic = _nextScene.Music;
                        _musicLoaded = true;
                        _musicVolume = 0f;
                        _targetMusicVolume = 1f;
                        Raylib.PlayMusicStream(_currentMusic);
                        Raylib.SetMusicVolume(_currentMusic, _musicVolume);
                    }

                    _nextScene = null!;
                }

                // Done fading out → fade in
                _fadingIn = true;
            }
            else return; // still fading out, skip rest of update
        }
        else if (_fadeAlpha > 0f)
        {
            _fadeAlpha -= _fadeSpeed * delta;
            if (_fadeAlpha < 0f) _fadeAlpha = 0f;
        }

        // --- Update the active scene ---
        if (_scenes.Count > 0)
        {
            var active = _scenes.Peek();
            active.Update();

            if (active.RequestExit) _scenes.Clear();
            if (active.RequestPop) Pop();
            if (active.RequestPush != null) Push(active.RequestPush);
            active.ResetRequests();
        }

        // --- Music fade + looping ---
        if (_musicLoaded)
        {
            Raylib.UpdateMusicStream(_currentMusic);

            if (_musicVolume < _targetMusicVolume)
            {
                _musicVolume += _musicFadeSpeed * delta;
                if (_musicVolume > _targetMusicVolume) _musicVolume = _targetMusicVolume;
            }
            else if (_musicVolume > _targetMusicVolume)
            {
                _musicVolume -= _musicFadeSpeed * delta;
                if (_musicVolume < _targetMusicVolume) _musicVolume = _targetMusicVolume;
            }
            Raylib.SetMusicVolume(_currentMusic, _musicVolume);

            if (!Raylib.IsMusicStreamPlaying(_currentMusic))
            {
                Raylib.PlayMusicStream(_currentMusic);
            }
        }
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
