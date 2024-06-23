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
		// GD.Print("on_timeout called");
		float randX = (float)GD.RandRange(0, 500);
		float randY = (float)GD.RandRange(0, 500);
		this.Position = new Vector2(randX, randY);
		this.EmitSignal("MovedWithArgument", randX, randY);
		//This is the same name as your Signal, just remove "EventHandler" from the end. 
	}

	void on_moved(float x, float y) {
		GD.Print("X: ",x, " Y: ",y);
	}
}
