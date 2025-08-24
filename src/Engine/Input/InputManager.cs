using Raylib_cs;

public class InputManager
{
    // Movement
    private KeyboardKey moveUpKey = KeyboardKey.W;
    private KeyboardKey moveDownKey = KeyboardKey.S;
    private KeyboardKey moveLeftKey = KeyboardKey.A;
    private KeyboardKey moveRightKey = KeyboardKey.D;

    // Arrow navigation
    private KeyboardKey arrowUpKey = KeyboardKey.Up;
    private KeyboardKey arrowDownKey = KeyboardKey.Down;
    private KeyboardKey arrowLeftKey = KeyboardKey.Left;
    private KeyboardKey arrowRightKey = KeyboardKey.Right;

    // Selection
    private KeyboardKey selectKey = KeyboardKey.Enter;
    private KeyboardKey returnKey = KeyboardKey.Escape;

    // Mouse
    private MouseButton leftClickButton = MouseButton.Left;
    private MouseButton rightClickButton = MouseButton.Right;

    // Hotbar
    private KeyboardKey hotbar1Key = KeyboardKey.One;
    private KeyboardKey hotbar2Key = KeyboardKey.Two;
    private KeyboardKey hotbar3Key = KeyboardKey.Three;
    private KeyboardKey hotbar4Key = KeyboardKey.Four;
    private KeyboardKey hotbar5Key = KeyboardKey.Five;
    private KeyboardKey hotbar6Key = KeyboardKey.Six;
    private KeyboardKey hotbar7Key = KeyboardKey.Seven;
    private KeyboardKey hotbar8Key = KeyboardKey.Eight;
    private KeyboardKey hotbar9Key = KeyboardKey.Nine;
    private KeyboardKey hotbar0Key = KeyboardKey.Zero;

    // Inventory
    private KeyboardKey inventoryKey = KeyboardKey.E;

    // ========= Public Rebindable methods =========

    // Movement
    public bool MoveUp() => Raylib.IsKeyDown(moveUpKey);
    public bool MoveDown() => Raylib.IsKeyDown(moveDownKey);
    public bool MoveLeft() => Raylib.IsKeyDown(moveLeftKey);
    public bool MoveRight() => Raylib.IsKeyDown(moveRightKey);

    // WASD Selector

    public bool MoveUpSelect() => Raylib.IsKeyPressed(moveUpKey);
    public bool MoveDownSelect() => Raylib.IsKeyPressed(moveDownKey);
    public bool MoveLeftSelect() => Raylib.IsKeyPressed(moveLeftKey);
    public bool MoveRightSelect() => Raylib.IsKeyPressed(moveRightKey);

    // Arrows
    public bool ArrowUp() => Raylib.IsKeyPressed(arrowUpKey);
    public bool ArrowDown() => Raylib.IsKeyPressed(arrowDownKey);
    public bool ArrowLeft() => Raylib.IsKeyPressed(arrowLeftKey);
    public bool ArrowRight() => Raylib.IsKeyPressed(arrowRightKey);

    // Selection
    public bool Select() => Raylib.IsKeyPressed(selectKey);
    public bool Return() => Raylib.IsKeyPressed(returnKey);

    // Mouse
    public bool LeftClick() => Raylib.IsMouseButtonPressed(leftClickButton);
    public bool RightClick() => Raylib.IsMouseButtonPressed(rightClickButton);

    // Hotbar
    public bool Hotbar1() => Raylib.IsKeyPressed(hotbar1Key);
    public bool Hotbar2() => Raylib.IsKeyPressed(hotbar2Key);
    public bool Hotbar3() => Raylib.IsKeyPressed(hotbar3Key);
    public bool Hotbar4() => Raylib.IsKeyPressed(hotbar4Key);
    public bool Hotbar5() => Raylib.IsKeyPressed(hotbar5Key);
    public bool Hotbar6() => Raylib.IsKeyPressed(hotbar6Key);
    public bool Hotbar7() => Raylib.IsKeyPressed(hotbar7Key);
    public bool Hotbar8() => Raylib.IsKeyPressed(hotbar8Key);
    public bool Hotbar9() => Raylib.IsKeyPressed(hotbar9Key);
    public bool Hotbar0() => Raylib.IsKeyPressed(hotbar0Key);

    // Inventory
    public bool OpenInventory() => Raylib.IsKeyPressed(inventoryKey);

    // ========= Rebinding =========

    // Movement
    public void RebindMoveUp(KeyboardKey newKey) => moveUpKey = newKey;
    public void RebindMoveDown(KeyboardKey newKey) => moveDownKey = newKey;
    public void RebindMoveLeft(KeyboardKey newKey) => moveLeftKey = newKey;
    public void RebindMoveRight(KeyboardKey newKey) => moveRightKey = newKey;

    // Arrows
    public void RebindArrowUp(KeyboardKey newKey) => arrowUpKey = newKey;
    public void RebindArrowDown(KeyboardKey newKey) => arrowDownKey = newKey;
    public void RebindArrowLeft(KeyboardKey newKey) => arrowLeftKey = newKey;
    public void RebindArrowRight(KeyboardKey newKey) => arrowRightKey = newKey;

    // Selection
    public void RebindSelect(KeyboardKey newKey) => selectKey = newKey;
    public void RebindReturn(KeyboardKey newKey) => returnKey = newKey;

    // Mouse
    public void RebindLeftClick(MouseButton newButton) => leftClickButton = newButton;
    public void RebindRightClick(MouseButton newButton) => rightClickButton = newButton;

    // Hotbar
    public void RebindHotbar1(KeyboardKey newKey) => hotbar1Key = newKey;
    public void RebindHotbar2(KeyboardKey newKey) => hotbar2Key = newKey;
    public void RebindHotbar3(KeyboardKey newKey) => hotbar3Key = newKey;
    public void RebindHotbar4(KeyboardKey newKey) => hotbar4Key = newKey;
    public void RebindHotbar5(KeyboardKey newKey) => hotbar5Key = newKey;
    public void RebindHotbar6(KeyboardKey newKey) => hotbar6Key = newKey;
    public void RebindHotbar7(KeyboardKey newKey) => hotbar7Key = newKey;
    public void RebindHotbar8(KeyboardKey newKey) => hotbar8Key = newKey;
    public void RebindHotbar9(KeyboardKey newKey) => hotbar9Key = newKey;
    public void RebindHotbar0(KeyboardKey newKey) => hotbar0Key = newKey;

    // Inventory
    public void RebindInventory(KeyboardKey newKey) => inventoryKey = newKey;

    // ====== Unrebindable ====== //

    public bool ScrollUp()
    {
        // Ignore Left Control because it will clash with Zoom In
        if (Raylib.GetMouseWheelMove() > 0 && !Raylib.IsKeyDown(KeyboardKey.LeftControl)) return true;
        return false;
    }

    public bool ScrollDown()
    {
        // Ignore Left Control because it will clash with Zoom Out
        if (Raylib.GetMouseWheelMove() < 0 && !Raylib.IsKeyDown(KeyboardKey.LeftControl)) return true;
        return false;
    }

    public bool ZoomIn()
    {
        if (Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.GetMouseWheelMove() > 0) return true;
        return false;
    }

    public bool ZoomOut()
    {
        if (Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.GetMouseWheelMove() < 0) return true;
        return false;
    }

    public bool SplitStackInHalf()
    {
        if (Raylib.IsKeyDown(KeyboardKey.LeftShift) && RightClick()) return true;
        return false;
    }
}
