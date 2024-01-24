using Godot;
using System;

public partial class EventManager : Node2D
{
    public static EventManager Instance;

    public event Action ScoreEvent;

    public override void _Ready()
    {
        // Ensure that this instance is the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            QueueFree();
        }
    }
    public void StartScoreEvent()
    {
        ScoreEvent?.Invoke();
    }
}
