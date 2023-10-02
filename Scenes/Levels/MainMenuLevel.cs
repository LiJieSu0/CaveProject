using Godot;
using System;

public partial class MainMenuLevel : Node2D
{
	public static Node ROOT;
    public override void _EnterTree()
    {
		ROOT=GetTree().Root;
    }
    public override void _Ready()
	{
		GD.Print(ROOT==null);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("test")){
			TransitionLayer tmp=(TransitionLayer)GetTree().Root.GetNode("TransitionLayer");
			CanvasLayer myCanvas=GetChild<CanvasLayer>(0);
			myCanvas.Visible=false;
			tmp.ChangeScene("res://Scenes/Levels/level_1.tscn");
		}
	}
}
