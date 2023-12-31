using Godot;

public class StartButton : Label
{
    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if (@event is not InputEventMouseButton { Pressed: true, ButtonIndex: (int)ButtonList.Left })
            return;

        GetTree().SetInputAsHandled();
        GetTree().ChangeScene("res://scenes/main/main.tscn");
    }
}
