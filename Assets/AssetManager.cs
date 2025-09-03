using Raylib_cs;

public static class AssetManager
{
    private static readonly string AssetsRoot = Path.Combine(AppContext.BaseDirectory, "Assets");

    // Track loaded assets so we can unload later
    private static readonly List<Texture2D> loadedTextures = new();
    private static readonly List<Font> loadedFonts = new();
    private static readonly List<Sound> loadedSounds = new();
    private static readonly List<Music> loadedMusic = new();

    public static Texture2D LoadTexture(string filename, int width = 0, int height = 0)
    {
        string path = Path.Combine(AssetsRoot, filename);
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: Asset not found: {path}");
            return new Texture2D(); // safe fallback
        }

        Image image = Raylib.LoadImage(path);
        // If width and height are provided, resize the image
        if (width > 0 && height > 0)
        {
            Raylib.ImageResize(ref image, width, height);
        }

        Texture2D texture = Raylib.LoadTextureFromImage(image); // Loads into VRAM
        Raylib.UnloadImage(image); // Unload from RAM, you still need to call UnloadTexture later

        loadedTextures.Add(texture);

        Console.WriteLine($"Loaded & resized texture: {filename} -> {texture.Width}x{texture.Height}");
        return texture;
    }

    public static Font LoadFont(string filename, int bakeSize = 16)
    {
        string path = Path.Combine(AssetsRoot, filename);
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: Font not found: {path}");
            return Raylib.GetFontDefault(); // fallback
        }

        Font font = Raylib.LoadFontEx(path, bakeSize, null, 0);
        loadedFonts.Add(font);

        Console.WriteLine($"Loaded font: {filename} (baked size: {bakeSize}px)");
        return font;
    }

    public static Sound LoadSound(string filename)
    {
        string path = Path.Combine(AssetsRoot, filename);
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: Sound not found: {path}");
            return new Sound(); // safe fallback
        }

        Sound sound = Raylib.LoadSound(path);
        loadedSounds.Add(sound);

        Console.WriteLine($"Loaded sound: {filename}");
        return sound;
    }

    public static Music LoadMusic(string filename)
    {
        string path = Path.Combine(AssetsRoot, filename);
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: Music not found: {path}");
            return new Music(); // safe fallback
        }

        Music music = Raylib.LoadMusicStream(path);
        loadedMusic.Add(music);

        Console.WriteLine($"Loaded music: {filename}");
        return music;
    }

    // --- UNLOAD ALL ---

    public static void UnloadAll()
    {
        foreach (var texture in loadedTextures)
            Raylib.UnloadTexture(texture);
        loadedTextures.Clear();

        foreach (var font in loadedFonts)
            Raylib.UnloadFont(font);
        loadedFonts.Clear();

        foreach (var sound in loadedSounds)
            Raylib.UnloadSound(sound);
        loadedSounds.Clear();

        foreach (var music in loadedMusic)
            Raylib.UnloadMusicStream(music);
        loadedMusic.Clear();

        Console.WriteLine("All assets unloaded.");
    }
}
