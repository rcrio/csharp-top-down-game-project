using Raylib_cs;

public static class AssetManager
{
    private static readonly string AssetsRoot =
    Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Assets");
    public static Texture2D LoadTexture(string relativePath, int width = 0, int height = 0)
    {
        string fullPath = Path.Combine(AssetsRoot, relativePath);
        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"ERROR: Texture not found: {fullPath}");
            return new Texture2D(); // Fallback
        }

        Image image = Raylib.LoadImage(fullPath); // Loads image into RAM

        // If width and height are specified, resize the image
        // Else, set it to original image size for logging
        if (width > 0 && height > 0)
        {
            Raylib.ImageResize(ref image, width, height);
        }
        else
        {
            width = image.Width;
            height = image.Height;
        }

        Texture2D texture = Raylib.LoadTextureFromImage(image); // Loads into VRAM
        Raylib.UnloadImage(image); // Unload image from RAM

        Console.WriteLine($"Loaded texture: {relativePath} -> {texture.Width}x{texture.Height} scaled at {width}x{height}");
        return texture;
    }

    public static Sound LoadSound(string relativePath)
    {
        string fullPath = Path.Combine(AssetsRoot, relativePath);
        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"ERROR: Sound not found: {fullPath}");
            return new Sound(); // Fallback
        }

        return Raylib.LoadSound(fullPath);
    }

    public static Font LoadFont(string relativePath, int bakeSize = 16)
    {
        string fullPath = Path.Combine(AssetsRoot, relativePath);
        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"ERROR: Font not found: {fullPath}");
            return Raylib.GetFontDefault(); // Fallback
        }
        return Raylib.LoadFontEx(fullPath, bakeSize, null, 0);
    }

    public static Music LoadMusic(string relativePath)
    {
        string fullPath = Path.Combine(AssetsRoot, relativePath);
        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"ERROR: Music not found: {fullPath}");
            return new Music(); // Fallback
        }

        return Raylib.LoadMusicStream(fullPath);
    }


    // Unload a texture
    public static void UnloadTexture(Texture2D texture)
    {
        Raylib.UnloadTexture(texture);
    }

    // Unload a sound
    public static void UnloadSound(Sound sound)
    {
        Raylib.UnloadSound(sound);
    }

    // Unload a font
    public static void UnloadFont(Font font)
    {
        Raylib.UnloadFont(font);
    }

    public static void UnloadMusic(Music music)
    {
        Raylib.UnloadMusicStream(music);
    }
}
