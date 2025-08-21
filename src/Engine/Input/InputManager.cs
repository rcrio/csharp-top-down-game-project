using Raylib_cs;
using System.Collections.Generic;

public class InputManager
{
    private Dictionary<Action, List<KeyboardKey>> _keyCombos;
    private Dictionary<Action, List<MouseButton>> _mouseCombos;

    public InputManager()
    {
        _keyCombos = new Dictionary<Action, List<KeyboardKey>>();
        _mouseCombos = new Dictionary<Action, List<MouseButton>>();

        SetDefaultBindings();
    }

    // Bind single key or combo
    public void BindKeys(Action action, params KeyboardKey[] keys)
    {
        _keyCombos[action] = new List<KeyboardKey>();
        for (int i = 0; i < keys.Length; i++)
        {
            _keyCombos[action].Add(keys[i]);
        }
    }

    public void BindMouseButtons(Action action, params MouseButton[] buttons)
    {
        _mouseCombos[action] = new List<MouseButton>();
        for (int i = 0; i < buttons.Length; i++)
        {
            _mouseCombos[action].Add(buttons[i]);
        }
    }

    // Action pressed this frame
    public bool IsActionPressed(Action action)
    {
        if (_keyCombos.ContainsKey(action))
        {
            List<KeyboardKey> keys = _keyCombos[action];
            bool allDown = true;
            bool onePressed = false;

            for (int i = 0; i < keys.Count; i++)
            {
                if (!Raylib.IsKeyDown(keys[i]))
                    allDown = false;
                if (Raylib.IsKeyPressed(keys[i]))
                    onePressed = true;
            }

            if (allDown && onePressed)
                return true;
        }

        if (_mouseCombos.ContainsKey(action))
        {
            List<MouseButton> buttons = _mouseCombos[action];
            bool allDown = true;
            bool onePressed = false;

            for (int i = 0; i < buttons.Count; i++)
            {
                if (!Raylib.IsMouseButtonDown(buttons[i]))
                    allDown = false;
                if (Raylib.IsMouseButtonPressed(buttons[i]))
                    onePressed = true;
            }

            if (allDown && onePressed)
                return true;
        }

        // Special cases
        if (action == Action.ZoomIn && Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.GetMouseWheelMove() > 0)
            return true;
        if (action == Action.ZoomOut && Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.GetMouseWheelMove() < 0)
            return true;
        
        if (action == Action.ScrollUp && Raylib.GetMouseWheelMove() > 0)
            return true;
        if (action == Action.ScrollDown && Raylib.GetMouseWheelMove() < 0)
            return true;

        return false;
    }

    // Action held down
    public bool IsActionDown(Action action)
    {
        if (_keyCombos.ContainsKey(action))
        {
            List<KeyboardKey> keys = _keyCombos[action];
            for (int i = 0; i < keys.Count; i++)
            {
                if (!Raylib.IsKeyDown(keys[i]))
                    return false;
            }
            return true;
        }

        if (_mouseCombos.ContainsKey(action))
        {
            List<MouseButton> buttons = _mouseCombos[action];
            for (int i = 0; i < buttons.Count; i++)
            {
                if (!Raylib.IsMouseButtonDown(buttons[i]))
                    return false;
            }
            return true;
        }

        return false;
    }

    public void SetDefaultBindings()
    {
        // Movement
        BindKeys(Action.MoveUp, KeyboardKey.W);
        BindKeys(Action.MoveDown, KeyboardKey.S);
        BindKeys(Action.MoveLeft, KeyboardKey.A);
        BindKeys(Action.MoveRight, KeyboardKey.D);

        // Arrow selection
        BindKeys(Action.Up, KeyboardKey.Up);
        BindKeys(Action.Down, KeyboardKey.Down);
        BindKeys(Action.Left, KeyboardKey.Left);
        BindKeys(Action.Right, KeyboardKey.Right);

        // Select and unselect
        BindKeys(Action.Select, KeyboardKey.Enter);
        BindKeys(Action.Return, KeyboardKey.Escape);
        BindMouseButtons(Action.OnClick, MouseButton.Left);

        // Hotbar
        BindKeys(Action.Hotbar1, KeyboardKey.One);
        BindKeys(Action.Hotbar2, KeyboardKey.Two);
        BindKeys(Action.Hotbar3, KeyboardKey.Three);
        BindKeys(Action.Hotbar4, KeyboardKey.Four);
        BindKeys(Action.Hotbar5, KeyboardKey.Five);
        BindKeys(Action.Hotbar6, KeyboardKey.Six);
        BindKeys(Action.Hotbar7, KeyboardKey.Seven);
        BindKeys(Action.Hotbar8, KeyboardKey.Eight);
        BindKeys(Action.Hotbar9, KeyboardKey.Nine);
        BindKeys(Action.Hotbar0, KeyboardKey.Zero);

        // Inventory
        BindKeys(Action.OpenInventory, KeyboardKey.E);
    }
}
