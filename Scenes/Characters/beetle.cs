using Godot;
using System;

public partial class beetle : CharacterBody2D
{

	NavigationAgent2D agent;

	public const float Speed = 300.0f;
	public int hitCount=0;
	RayCast2D sight;
	private bool isDetectingPlayer=false;

	
	
	private bool isDashing=false;
	private bool canDash=true;
	Timer dashTimer;
	public override void _Ready()
	{
		bullet.bulletHit+=hitByBullet;
		sight=GetNode<RayCast2D>("RayCast2D");
		dashTimer=GetNode<Timer>("DashTimer");
		agent=GetNode<NavigationAgent2D>("NavigationAgent2D");
	}
	
	public override void _Process(double delta)
	{
		// if(Input.IsActionJustPressed("test")){
		// 	isDashing=true;
		// 	canDash=false;
		// 	dashTimer.Start();
		// }
		// if(isDashing){
		// 	Velocity+=(float)delta*Vector2.Right*5000;
		// 	MoveAndSlide();
		// }
		agent.TargetPosition=GetParent().GetNode<Node2D>("Player").Position;

		var direction=agent.GetNextPathPosition()-GlobalPosition;
		direction=direction.Normalized();
		Velocity=Velocity.Lerp(direction*Speed,20*(float)delta);

		MoveAndSlide();
		
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

			}
		}
	}
	public void OnDashTimerTimeout(){
		isDashing=false;
		canDash=true;
	}
}
