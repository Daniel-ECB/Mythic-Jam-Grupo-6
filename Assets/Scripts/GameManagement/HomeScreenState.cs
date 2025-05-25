using MythicGameJam.Core.GameManagement;

public sealed class HomeScreenState : IGameState
{
    private readonly GameManager _gameManager;

    public HomeScreenState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        // Show main menu, pause gameplay, etc.
    }

    public void Exit()
    {
        // Hide menu, cleanup, etc.
    }

    public void Update()
    {
        // Handle input for starting the game, etc.
    }
}
