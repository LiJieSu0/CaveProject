using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	
	AnimatedSprite2D animationPlayer;
	AudioStreamPlayer2D shootSEPlayer;
	private	bool isReloading=false;
	private bool isShooting=false;
	private bool canShoot=true;
	private PackedScene bulletScene;
	public override void _Ready()
	{
		animationPlayer=GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		shootSEPlayer=GetNode<AudioStreamPlayer2D>("ShootSEPlayer");
		bulletScene=(PackedScene)GD.Load("res://Scenes/Characters/bullet.tscn");
	}
	public override void _Process(double delta)
	{
		MovingFunc();
		CharacterState();
		for(int i=0;i<GetSlideCollisionCount();i++){
			var collision=GetSlideCollision(i);
			GD.Print((collision.GetCollider() as Node2D).Name);
		}


	}
	private void MovingFunc(){
		LookAt(GetGlobalMousePosition());
		Vector2 direction=Input.GetVector("left","right","up","down");
		Velocity=direction*Speed;
		MoveAndSlide();
		
	}
	private async void OnReloadFinished(){
		animationPlayer.Play("Reload");
		await ToSignal(animationPlayer,"animation_finished");
		GD.Print("Reload complete");
		isReloading=false;
	}

	private async void CharacterState(){
		if(Input.IsActionJustPressed("reload")){
			isReloading=true;
			GD.Print("reload");
			OnReloadFinished();
		}
		else if(Input.IsActionJustPressed("shoot")&&!isReloading&&canShoot){
			isShooting=true;
			animationPlayer.Play("Shoot");
			shootSEPlayer.Play();
			BulletInstantiate();
			await ToSignal(animationPlayer,"animation_finished");
			isShooting=false;
			canShoot=false;
			Timer shootTimer=GetNode<Timer>("ShootTimer");
			shootTimer.Start();
		}
		else if(Velocity==Vector2.Zero&&!isReloading&&!isShooting){
			animationPlayer.Play("Idle");
		}
		else if(Velocity!=Vector2.Zero&&!isReloading&&!isShooting){
			animationPlayer.Play("Moving");
		}
	}
	private void BulletInstantiate(){
        Godot.Collections.Array<Node> nodes = GetNode("Muzzle").GetChildren();
		int randomInt=GD.RandRange(0,nodes.Count-1);
		bullet tmp=(bullet)bulletScene.Instantiate();
		tmp.setDirection((GetGlobalMousePosition()-this.Position).Normalized());
		tmp.GlobalPosition=((Node2D)nodes[randomInt]).GlobalPosition;
		tmp.Rotation=Rotation;
		GetParent().AddChild(tmp);
	}
	private void OnShootTimerTimeout(){
		canShoot=true;
	}
}
