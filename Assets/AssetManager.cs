using Raylib_cs;

public static class AssetManager
{
    private static readonly string AssetsRoot = Path.Combine(AppContext.BaseDirectory, "Assets");

    public static Texture2D LoadTexture(string filename)
    {
        string path = Path.Combine(AssetsRoot, filename);
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: Asset not found: {path}");
            return new Texture2D(); // safe fallback
        }

        //Texture2D texture = Raylib.LoadTexture(path);
        //Console.WriteLine($"Loaded texture: {filename} ({texture.Width}x{texture.Height})");
        //return texture;

        // Load full texture
        Image image = Raylib.LoadImage(path);

        // Resize to desired size (in-place)
        Raylib.ImageResize(ref image, 16, 16);

        // Convert resized image into a texture
        Texture2D texture = Raylib.LoadTextureFromImage(image);

        // Unload CPU-side image (keep GPU texture only)
        Raylib.UnloadImage(image);

        Console.WriteLine($"Loaded & resized texture: {filename} -> {16}x{16}");
        return texture;
    }

    // Load font at high resolution for sharp scaling
    public static Font LoadFont(string filename, int bakeSize = 16)
    {
        string path = Path.Combine(AssetsRoot, filename);
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: Font not found: {path}");
            return Raylib.GetFontDefault(); // fallback to default font
        }

        // Bake font at specified size (default 64px) for crisp text
        Font font = Raylib.LoadFontEx(path, bakeSize, null, 0);
        Console.WriteLine($"Loaded font: {filename} (baked size: {bakeSize}px)");
        return font;
    }
}