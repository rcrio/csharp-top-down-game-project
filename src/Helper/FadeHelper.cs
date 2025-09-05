using Raylib_cs;

class Fader
{
    private float alpha;
    private float fadeSpeed;
    public Fader(float speed = 0.01f)
    {
        fadeSpeed = speed;
        alpha = 0f; // start fully transparent
    }
    public bool FadeOut()
    {
        alpha += fadeSpeed;   // fade out: alpha 0 → 1
        if (alpha >= 1f)
        {
            alpha = 1f;
            return true;
        }
        return false;
    }

    public bool FadeIn()
    {
        alpha -= fadeSpeed;   // fade in: alpha 1 → 0
        if (alpha <= 0f)
        {
            alpha = 0f;
            return true;
        }
        return false;
    }

    public void Draw()
    {
        Color tint = new Color((byte)0, (byte)0, (byte)0, (byte)(alpha * 255));
        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), tint);
    }
}
