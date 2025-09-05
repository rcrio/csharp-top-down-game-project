using System.Runtime.CompilerServices;
using Raylib_cs;

public class SceneManager
{
    private Stack<Scene> _scenes;
    private Scene _nextSceneToPush;
    private bool _shouldPop;
    private bool _shouldExit;
    private bool _shouldFade;

    // Fading variables
    private Fader _fader;
    private bool _fadingBlackIn = false;
    private bool _fadingBlackOut = false;
    private bool _transitioning = false;
    private Music _currentMusic;

    public SceneManager(InputManager inputManager, GameTime gameTime)
    {
        _scenes = new Stack<Scene>();
        var mainMenuScene = new MainMenuScene(inputManager, gameTime);
        mainMenuScene.Load();
        _scenes.Push(mainMenuScene);
        // Set the current music to the main menu music
        _currentMusic = mainMenuScene.Music;
        // Start playing it. This doesn't play automatically, we need to call UpdateMusicStream in the update loop
        Raylib.PlayMusicStream(_currentMusic);
        _shouldExit = false;
        Console.WriteLine("Called!");
    }

    public void Push(Scene scene)
    {
        _nextSceneToPush = scene;
    }

    public void Pop()
    {
        _shouldPop = true;
    }

    public void RequestExit()
    {
        _shouldExit = true;
    }

    public void Update()
    {
        if (_scenes.Count == 0) return;

        Scene active = _scenes.Peek();

        
        // Only actively update active scene if not transitioning
        if (!_transitioning)
        {
            active.Update();
            // Plays music stream
            Raylib.UpdateMusicStream(_currentMusic);

            // Handle scene requests
            if (active.RequestExit) _shouldExit = true;
            if (active.RequestFade) _shouldFade = true;
            if (active.RequestPop) _shouldPop = true;
            if (active.RequestPush != null) _nextSceneToPush = active.RequestPush;

            active.ResetRequests();

            if (_shouldPop)
            {
                _transitioning = true; // start transition
                if (_shouldFade) _fadingBlackIn = true;
            }

            if (_nextSceneToPush != null)
            {
                _transitioning = true; // start transition
                if (_shouldFade) _fadingBlackIn = true;
            }
        }  

        
        // Transitioning without fade
        if (_transitioning && !_shouldFade)
        {
            // No fading, just do the scene switch immediately
            if (_shouldPop)
            {
                _scenes.Peek().Unload();
                _scenes.Pop();
                _shouldPop = false;
                if (_scenes.Count > 0)
                {
                    _scenes.Peek().Load();
                    _scenes.Peek().Update();
                }
                _transitioning = false;
            }

            if (_nextSceneToPush != null)
            {
                _nextSceneToPush.Load();
                _scenes.Push(_nextSceneToPush);
                _nextSceneToPush.Update();
                _nextSceneToPush = null;
                _transitioning = false;
            }
        }

        // Transitioning with fasde
        if (_transitioning && _shouldFade && _fader == null)
        {
            // Set this flag to true to start fading to black
            _fader = new Fader(0.02f);
        }

        // Fading logic only works if fader exists
        if (_shouldFade && _fader != null)
        {
            if (_fadingBlackIn)
            {
                Console.WriteLine("Fading out");
                if (_fader.FadeOut())
                {
                    if (_shouldPop)
                    {
                        _scenes.Peek().Unload();
                        _scenes.Pop();
                        _shouldPop = false;
                        _scenes.Peek().Load();
                        _scenes.Peek().Update();
                    }

                    if (_nextSceneToPush != null)
                    {
                        _nextSceneToPush.Load();
                        _scenes.Push(_nextSceneToPush);
                        _nextSceneToPush.Update();
                        _nextSceneToPush = null;
                    }
                    _fadingBlackIn = false;
                    _fadingBlackOut = true;
                }
            }
            if (_fadingBlackOut)
            {
                Console.WriteLine("Fading back in");
                if (_fader.FadeIn())
                {
                    _fadingBlackOut = false;
                }
            }
            // Fader has completed fading, we set it to null to continue regular scene logic
            if (!_fadingBlackIn && !_fadingBlackOut)
            {
                _transitioning = false;
                _shouldFade = false;
                _fader = null;
            }
        }   
    }

    public void Draw()
    {
        if (_scenes.Count == 0) return;

        _scenes.Peek().Draw();

        if (_fader != null)
        {
            _fader.Draw();

        }
    }

    public Scene GetActiveScene()
    {
        return _scenes.Count > 0 ? _scenes.Peek() : null;
    }

    public bool ShouldClose()
    {
        return _shouldExit || _scenes.Count == 0;
    }
}
