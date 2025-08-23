// dude, fuck knows this was gpt'd
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

        Texture2D texture = Raylib.LoadTexture(path);
        Console.WriteLine($"Loaded texture: {filename} ({texture.Width}x{texture.Height})");
        return texture;
    }
}
