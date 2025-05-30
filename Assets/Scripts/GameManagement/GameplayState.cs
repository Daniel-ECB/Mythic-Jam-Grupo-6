using MythicGameJam.Input;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class GameplayState : IGameState
    {
        private readonly GameManager _gameManager;

        public GameplayState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Enter()
        {
            InputManager.Instance.OnPause += HandlePause;
        }

        public void Exit()
        {
            if (InputManager.Instance != null)
                InputManager.Instance.OnPause -= HandlePause;
        }

        public void Update()
        {
            // Handle gameplay logic, player input, etc.
        }

        private void HandlePause()
        {
            _gameManager.PauseGame(() => MythicGameJam.UI.Menus.GameplayMenuManager.Instance.ShowSubmenu(
                    MythicGameJam.UI.Menus.GameplayMenuManager.SubmenuType.PauseMenu));
        }
    }
}
