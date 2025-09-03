using System.Numerics;
using Raylib_cs;

public class GameClock
{
    public int Hour { get; private set; } = 6;
    public int Minute { get; private set; } = 0;
    public int Day { get; private set; } = 1;
    public int Season { get; private set; } = 1;
    public int Year { get; private set; } = 1;

    private float accumulator = 0f;
    private float secondsPerGameMinute = 1f; // tweak speed here
    private Font _font;

    public void Update(float deltaTime)
    {
        accumulator += deltaTime;

        while (accumulator >= secondsPerGameMinute)
        {
            accumulator -= secondsPerGameMinute;
            AdvanceMinute();
        }
    }

    public void DrawClock()
    {
        // refactor, needs constant fontSize. actually if u read this, make all the other fontsizes constant;
        float fontSize = 16;   // desired text size for drawing
        float spacing = 1;     // optional letter spacing
        float lineHeight = fontSize + 4; // space between lines

        // Start from top-right corner
        Vector2 pos = new Vector2(Raylib.GetScreenWidth() - 10, 10);

        // Time string
        string timeText = $"Time: {Hour:D2}:{Minute:D2}";
        Vector2 timeSize = Raylib.MeasureTextEx(_font, timeText, fontSize, spacing);
        pos.X -= timeSize.X; // align right
        Raylib.DrawTextEx(_font, timeText, pos, fontSize, spacing, Color.White);

        // Move down for date
        pos.Y += lineHeight;

        string dateText = GetDateString();
        Vector2 dateSize = Raylib.MeasureTextEx(_font, dateText, fontSize, spacing);
        pos.X = Raylib.GetScreenWidth() - dateSize.X - 10; // align right again
        Raylib.DrawTextEx(_font, dateText, pos, fontSize, spacing, Color.White);
    }

    public void Load()
    {
        _font = FontHandler.GetFontNormal(); // Don't need to call unload, font handler does this in the main game class
    }

    public void Unload()
    {
        
    }
    private void AdvanceMinute()
    {
        Minute++;
        if (Minute >= 60)
        {
            Minute = 0;
            Hour++;
        }
        if (Hour >= 24)
        {
            Hour = 0;
            NextDay();
        }
    }

    private void NextDay()
    {
        Day++;
        if (Day > 28)
        {
            Day = 1;
            Season++;
            if (Season > 4)
            {
                Season = 1;
                Year++;
            }
        }
    }

    private string GetSeasonName()
    {
        return Season switch
        {
            1 => "Spring",
            2 => "Summer",
            3 => "Fall",
            4 => "Winter",
            _ => "Unknown"
        };
    }

    public string GetTimeString() => $"{Hour:D2}:{Minute:D2}";
    public string GetDateString() => $"Day {Day}, {GetSeasonName()}, Year {Year}";
    
}
