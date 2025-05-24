using MythicGameJam.Core.GameManagement;

public sealed class GameplayState : IGameState
{
    private readonly GameManager _gameManager;
    public GameplayState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        // Start gameplay, initialize game objects, etc.
    }

    public void Exit()
    {
        // Pause gameplay, save state, etc.
    }

    public void Update()
    {
        // Handle gameplay logic, player input, etc.
    }
}
