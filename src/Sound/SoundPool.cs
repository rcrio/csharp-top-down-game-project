using Raylib_cs;
using System;

public class SoundPool
{
    private Sound[] _sounds;
    private int _current = 0;

    public SoundPool(string filename, int poolSize = 8)
    {
        _sounds = new Sound[poolSize];

        // Load the first sound (owns sample)
        _sounds[0] = AssetManager.LoadSound(filename);

        // Clone the sound for aliases
        for (int i = 1; i < poolSize; i++)
        {
            // In Raylib C#, we just reload the sound. It's lightweight for short effects.
            _sounds[i] = AssetManager.LoadSound(filename);
        }
    }

    public void Play()
    {
        Raylib.PlaySound(_sounds[_current]);
        _current = (_current + 1) % _sounds.Length;
    }

    public void Unload()
    {
        foreach (var s in _sounds)
            Raylib.UnloadSound(s);
    }
}
