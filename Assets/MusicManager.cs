using Raylib_cs;

public class MusicManager
{
    private Music _current;
    private string _currentFile = null; // track currently playing
    private float _volume = 1f;
    private float _targetVolume = 1f;
    private float _fadeSpeed = 0f;

    private Dictionary<string, Music> _cache = new();

    public void Play(string filename, float fadeInSeconds = 1f)
    {
        // If same track already active, do nothing
        if (_currentFile == filename && _current.Stream.Buffer != System.IntPtr.Zero)
            return;

        // Fade out current track if exists
        Stop(fadeInSeconds);

        // Load or fetch from cache
        if (!_cache.ContainsKey(filename))
        {
            Music loaded = AssetManager.LoadMusic(filename);
            if (loaded.Equals(default(Music)))
            {
                System.Console.WriteLine($"[MusicManager] Failed to load: {filename}");
                return;
            }
            _cache[filename] = loaded;
        }

        _current = _cache[filename];
        _currentFile = filename;

        Raylib.PlayMusicStream(_current);

        // Start from 0, fade in
        _volume = 0f;
        _targetVolume = 1f;
        _fadeSpeed = fadeInSeconds > 0 ? 1f / fadeInSeconds : 1f;
    }

    public void Stop(float fadeOutSeconds = 1f)
    {
        if (_current.Stream.Buffer == System.IntPtr.Zero) return;

        _targetVolume = 0f;
        _fadeSpeed = fadeOutSeconds > 0 ? 1f / fadeOutSeconds : 1f;
    }

    public void Update(float dt)
    {
        if (_current.Stream.Buffer == System.IntPtr.Zero) return;

        Raylib.UpdateMusicStream(_current);

        // Smooth fade
        if (_volume < _targetVolume)
        {
            _volume += _fadeSpeed * dt;
            if (_volume > _targetVolume) _volume = _targetVolume;
        }
        else if (_volume > _targetVolume)
        {
            _volume -= _fadeSpeed * dt;
            if (_volume < _targetVolume) _volume = _targetVolume;
        }

        Raylib.SetMusicVolume(_current, _volume);

        // If fully faded out, stop and clear
        if (_volume == 0f && _targetVolume == 0f)
        {
            Raylib.StopMusicStream(_current);
            _currentFile = null;
        }
    }

    public void UnloadAll()
    {
        foreach (var kv in _cache)
            Raylib.UnloadMusicStream(kv.Value);

        _cache.Clear();
        _currentFile = null;
    }
}
