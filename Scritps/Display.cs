using CommunityToolkit.Mvvm.Messaging;
using Godot;

public partial class Display : CanvasLayer, IRecipient<PlayerSpeedMessage>
{
    private int _playerScore;
    private bool isScoreIncrease = false;
    [Export] public Label PlayerSpeed;
    [Export] public Label PlayerScore;

    public override void _Ready()
    {
        EventManager.Instance.ScoreEvent += UpdateScore;
    }
    public override void _Process(double delta)
    {
        if (isScoreIncrease)
        {
            PlayerScore.Text = _playerScore.ToString();
            isScoreIncrease = false;
        }
    }
    private void UpdateScore()
    {
        _playerScore += 1;
        isScoreIncrease = true;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        EventManager.Instance.ScoreEvent -= UpdateScore;
        StrongReferenceMessenger.Default.UnregisterAll(this);
    }
    public override void _EnterTree()
    {
        base._EnterTree();
        StrongReferenceMessenger.Default.RegisterAll(this);


    }
    public void Receive(PlayerSpeedMessage message)
    {
        PlayerSpeed.Text = message.Player.ToString("0Km/h");
    }

}
