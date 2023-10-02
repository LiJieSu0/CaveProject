using Godot;
using System;

public partial class TransitionLayer : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public async void ChangeScene(String targetScene){
		AnimationPlayer tmp=GetNode<AnimationPlayer>("AnimationPlayer");
		tmp.Play("FadeToBlack");
		await ToSignal(tmp,"animation_finished");
		GetTree().ChangeSceneToFile(targetScene);
	}

}
