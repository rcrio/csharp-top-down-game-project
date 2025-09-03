using Raylib_cs;

public static class FontHandler
{
    public static Font FontNormal;
    public static Font FontMenu;
    public static void LoadFont()
    {
        FontNormal = AssetManager.LoadFont("Fonts/Roboto-Regular.ttf", 16);
        Raylib.SetTextureFilter(FontNormal.Texture, TextureFilter.Bilinear);
        FontMenu = AssetManager.LoadFont("Fonts/ferrum.otf", 48);
        Raylib.SetTextureFilter(FontNormal.Texture, TextureFilter.Bilinear);
    }

    public static Font GetFontNormal()
    {
        return FontNormal;
    }

    public static Font GetFontMenu()
    {
        return FontMenu;
    }

    public static void UnloadFont()
    {
        Raylib.UnloadFont(FontNormal);
    }
}