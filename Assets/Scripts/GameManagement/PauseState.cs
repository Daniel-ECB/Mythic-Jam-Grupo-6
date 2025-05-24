using MythicGameJam.Core.GameManagement;

public sealed class PauseState : IGameState
{
    private readonly GameManager _gameManager;
    public PauseState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        // Show pause menu, freeze gameplay, etc.
    }

    public void Exit()
    {
        // Hide pause menu, resume gameplay, etc.
    }

    public void Update()
    {
        // Handle input for resuming the game, quitting to main menu, etc.
    }
}
