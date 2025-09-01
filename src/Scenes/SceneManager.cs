// SceneManager handles scene creation, drawing, and transitions between scenes.
// It does NOT handle input itself; input is passed to scenes through other managers.
public class SceneManager
{
    private InputManager _inputManager;
    private GameTime _gameTime;
    private MusicManager _musicManager;
    private Stack<Scene> _scenes; // Stack to hold all active scenes. Top of the stack is the currently active scene.

    private Scene _nextSceneToPush; // Holds a scene that should be pushed at the end of the current frame.
    
    private bool _shouldPop; // Flag indicating that the top scene should be popped after the current update.

    private bool _shouldExit; // Flag indicating that the game should exit.

    // could refactor by using inputManager and gameTime in variables here by using constructor DI instead of passing it thorugh completely

    

    // Constructor: initializes the stack and pushes the initial scene (main menu)
    public SceneManager(InputManager inputManager, GameTime gameTime)
    {
        _inputManager = inputManager;
        _gameTime = gameTime;
        _musicManager = new MusicManager();
        _scenes = new Stack<Scene>();

        // Push the main menu scene as the first active scene
        _scenes.Push(new MainMenuScene(_inputManager, _gameTime, _musicManager));

        _shouldExit = false; // Game does not exit immediately
        
        
    }

    // Request to push a new scene. It will not be pushed immediately, but after the current update.
    public void Push(Scene scene)
    {
        _nextSceneToPush = scene;
    }

    // Request to pop the current scene. Actual pop happens after the current update.
    // This prevents modifying the stack in the middle of a scene's Update() call.
    public void Pop()
    {
        _shouldPop = true;
    }

    // Request to exit the game. Will be checked in ShouldClose().
    public void RequestExit()
    {
        _shouldExit = true;
    }

    // Updates the active scene and handles queued scene operations.
    public void Update()
    {
        if (_scenes.Count == 0) return;

        Scene active = _scenes.Peek();

        // Update the active scene first. GameUpdate -> SceneManagerUpdate -> SceneUpdate
        active.Update();

        // Update music before potentially changing scenes
        _musicManager.Update(_gameTime.DeltaTime);

        // Handle any requests made by the active scene
        if (active.RequestExit) _shouldExit = true;          // Scene requested game exit. Exit the game completely
        if (active.RequestPop) _shouldPop = true;           // Scene requested itself to pop. Go back a scene
        if (active.RequestPush != null)                    // Scene requested to push a new scene. Transition to new scene
            _nextSceneToPush = active.RequestPush;

        // Reset scene requests so they don't persist into the next frame
        active.ResetRequests();

        // Apply queued changes AFTER updating the active scene
        // This ensures scenes are not popped/pushed in the middle of their own update
        if (_shouldPop)
        {
            active.Unload();    // Call unload on the scene before removing it
            _scenes.Pop();      // Remove the scene from the stack
            _shouldPop = false; // Reset the flag
        }

        if (_nextSceneToPush != null)
        {
            _scenes.Push(_nextSceneToPush); // Push new scene onto the stack
            _nextSceneToPush = null;        // New scene has no new scene to push so we set it to null
        }
    }

    // Draw the active scene (top of the stack)
    public void Draw()
    {
        if (_scenes.Count == 0) return;
        _scenes.Peek().Draw();
    }

    // Returns the currently active scene, or null if no scenes exist
    public Scene GetActiveScene()
    {
        return _scenes.Count > 0 ? _scenes.Peek() : null;
    }

    // Determines if the game should close:
    // Either the exit flag is set, or there are no more scenes left in the stack
    public bool ShouldClose()
    {
        return _shouldExit || _scenes.Count == 0;
    }
}
