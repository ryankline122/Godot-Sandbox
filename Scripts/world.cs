using Godot;
using System;

public partial class world : Node2D
{
	PackedScene ps;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ps = (PackedScene)GD.Load("res://Scenes/sprite_2d.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Event listener
	// public override void _UnhandledInput(InputEvent @event)
	// {
	// 	if (@event is InputEventMouseButton me) {
	// 		GD.Print("INFO: Logging works :)");
	// 		Sprite2D sprite = (Sprite2D)ps.Instantiate();
	// 		sprite.Position = me.Position;
	// 		this.AddChild(sprite);
	// 	}
	// }

}
