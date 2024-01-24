using CommunityToolkit.Mvvm.Messaging;
using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
    [Export]
    public float Speed = 1200f;
    [Export]
    private float acceleration = 500f;
    [Export]
    private float driftMultiplier = 0.5f; // Drift effect multiplier
    [Export]
    private float drag = 600f;
    [Export]
    private float maxSpeed = 2000f;
    [Export]
    private float turnSpeed = .1f;

    private float currentSpeed = 0f;

    public override void _Process(double delta)
    {
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        GetMoveAction(direction, Velocity, delta);

        MoveAndSlide();
    }

    private void GetMoveAction(Vector2 direction, Vector2 velocity, double delta)
    {
		// Allow drift control with spacebar
        if (Input.IsActionPressed("ui_accept"))
        {
            float driftDirection = Input.GetActionStrength("ui_accept");
            direction.X += driftDirection * Mathf.Sign(direction.X) * driftMultiplier;
        }
        if (currentSpeed != 0)
            Rotation += direction.X * turnSpeed;

        float angle = Rotation;

        // Acceleration and drag logic
        if (direction.Y != 0)
        {
            float targetSpeed = direction.Y > 0 ? maxSpeed : -maxSpeed;
            currentSpeed = Mathf.MoveToward(currentSpeed, targetSpeed, acceleration * (float)delta);
        }
        else
        {
            // Apply drag when not accelerating
            currentSpeed = Mathf.MoveToward(currentSpeed, 0f, drag * (float)delta);
        }
		
        velocity.X = Mathf.Cos(angle) * currentSpeed;
        velocity.Y = Mathf.Sin(angle) * currentSpeed;
        Velocity = velocity;

         // Convert and print speed in km/h
        float speedKMH = currentSpeed * 0.036f;
        StrongReferenceMessenger.Default.Send<PlayerSpeedMessage>(new(Mathf.Abs(speedKMH)));
    }
}
