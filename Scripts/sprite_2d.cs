using Godot;

public partial class sprite_2d : Sprite2D
{
	// Creating our own signal
	[Signal]
	public delegate void MovedWithArgumentEventHandler(float x, float y);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer timer = this.GetNode<Timer>("Timer");
		timer.WaitTime = 1;
		// Slots - Calls a method when a specific criteria is met
		// ex. When timer ends, call some custom method
		timer.Timeout += () => on_timeout();
		timer.Start();

		this.MovedWithArgument += on_moved;	
	}

	/*
		User Input:

		How It Works
			- Input Event Flow: 
				In Godot, input events are propagated through the scene tree, 
				starting from the root node. Each node has the opportunity to "consume" the event 
				by marking it as handled. If a node handles the event (e.g., by using the _input(event) 
				function), it will not propagate further.
			- Unhandled Input: 
				If an input event goes through the entire scene tree without being consumed, it reaches 
				unhandled_input. This function can be overridden in any node to catch these unhandled 
				events.

		When to Use unhandled_input
			- Global Shortcuts: 
				It's ideal for global actions like opening a pause menu with the escape button. 
				Since it only receives events that were not consumed by any other node, it ensures 
				that your pause functionality won't interfere with other input-handling nodes.
			- Fallback Input Handling: 
				In complex scenes where input might be handled differently depending on the context 
				(e.g., different UI panels, game modes), unhandled_input can serve as a catch-all for 
				inputs that don't have a specific handler elsewhere.
			- Camera Controls or Background Actions: 
				For actions that should be available regardless of the player's current interaction, 
				such as camera zoom or background music adjustment.
	*/

	// If we use this, the sprite will move when we try clicking the button
	// public override void _Input(InputEvent @event)
	// {
	// 	  if (@event is InputEventMouseButton me) {
	// 		if (me.Pressed) {
	// 			this.Position = me.GlobalPosition;
	// 		}
	// 	  }
	// }

	// This will not move the sprite when clicking GUI button
	public override void _UnhandledInput(InputEvent @event)
	{
		  if (@event is InputEventMouseButton me) {
			if (me.Pressed) {
				this.Position = me.GlobalPosition;
			}
		  }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Accessing child nodes (assuming of type sprite2D)
		// Sprite2D child = this.GetNode<Sprite2D>("Sprite2D");

		uint rng = GD.Randi() % 4;
		float AMOUNT = 5;
		if (rng == 0) {
			this.Position += new Vector2(0, -AMOUNT);
		}
		else if (rng == 1) {
			this.Position += new Vector2(0, AMOUNT);
		}
		else if (rng == 2) {
			this.Position += new Vector2(-AMOUNT, 0);
		}
		else if (rng == 3) {
			this.Position += new Vector2(AMOUNT, 0);
		}
	}

	void on_timeout() {
		float randX = (float)GD.RandRange(0, 500);
		float randY = (float)GD.RandRange(0, 500);
		this.Position = new Vector2(randX, randY);
		this.EmitSignal("MovedWithArgument", randX, randY);
	}

	void on_moved(float x, float y) {
		GD.Print("X: ",x, " Y: ",y);
	}
}
