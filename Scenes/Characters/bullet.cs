using Godot;
using System;

public partial class bullet : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public delegate void BulletHit(Node2D body);
	public static BulletHit bulletHit;
	public float Speed=300;
	Vector2 direction;

	public void setDirection(Godot.Vector2 dir){
		direction=dir;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position+=direction*Speed*(float)delta;
	}
	public void OnBodyEntered(Node2D body){
		bulletHit(body);
        QueueFree();
	}
}
