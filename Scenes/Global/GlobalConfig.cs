using Godot;
using System;

public partial class GlobalConfig:Node{
    public static Node ROOT;

    public override void _EnterTree(){
        ROOT=GetTree().Root;
    }
    public override void _Ready()
    {
        GD.Print(ROOT==null);
    }

}