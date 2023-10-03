using Godot;
using System;

public partial class beetle : CharacterBody2D
{
	public const float Speed = 300.0f;
	public int hitCount=0;
	RayCast2D sight;
	private bool isDetectingPlayer=false;
	public override void _Ready()
	{
		bullet.bulletHit+=hitByBullet;
		sight=GetNode<RayCast2D>("RayCast2D");
	}
	
	public override void _Process(double delta)
	{
		SightDetection();
		if(isDetectingPlayer)
			LookAt(GetParent().GetNode<Node2D>("Player").Position);
		//TODO patrol AI, follow AI, Attack AI	
	}
	public void hitByBullet(Node2D body){
		if(body==this){
			hitCount++;
			if(hitCount>=3){
				QueueFree();
			}
		}
	}

	public void OnBodyEnterDetectArea(Node2D body){
		if(body.Name=="Player"){
			isDetectingPlayer=true;
		}
	}
	public void OnBodyExitDetectArea(Node2D body){
		if(body.Name=="Player"){
			isDetectingPlayer=false;
		}
	}
	public void SightDetection(){
		if(sight.GetCollider()!=null){
			Node2D tmp=(Node2D)sight.GetCollider();
			if(tmp.Name=="Player"){
				GD.Print(this.Name+" Found Player");
			}
		}
	}
}
