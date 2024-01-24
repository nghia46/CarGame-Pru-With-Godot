using Godot;

public partial class GameManager : Node2D
{
	
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_accept"))
		{
			EventManager.Instance.StartScoreEvent();
		}
	}
}
