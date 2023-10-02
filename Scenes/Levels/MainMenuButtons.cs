using Godot;
using System;

public partial class MainMenuButtons : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public async void OnPlayBtnPressed(){
		TransitionLayer tmp=(TransitionLayer)GetTree().Root.GetNode("TransitionLayer");
		CanvasLayer myCanvas=GetTree().Root.GetNode("MainMenu").GetChild<CanvasLayer>(0);
		AudioStreamPlayer2D BGMPlayer=GetTree().Root.GetNode("MainMenu").GetNode<AudioStreamPlayer2D>("BGMPlayer");
		BGMPlayer.Stop();
		BGMPlayer.Stream=(AudioStream)ResourceLoader.Load("res://Assests/Audio/SE/EnterGame.mp3");
		BGMPlayer.Play();
		await ToSignal(BGMPlayer,"finished");
		myCanvas.Visible=false;
		tmp.ChangeScene("res://Scenes/Levels/level_1.tscn");//tmp for enter game
	}
	public void OnOptionBtnPressed(){
		//TODO show options menu;		
	}
	public void OnExitBtnPressed(){
		//TODO close the game;
	}
}
