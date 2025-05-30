using MythicGameJam.UI.Menus;
using UnityEngine;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class PauseState : IGameState
    {
        private readonly GameManager _gameManager;

        public PauseState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Enter()
        {
            _gameManager.IsPaused = true;
            Time.timeScale = 0f;
            GameplayMenuManager.Instance.ShowSubmenu(GameplayMenuManager.SubmenuType.PauseMenu);
        }

        public void Exit()
        {
            _gameManager.IsPaused = false;
            Time.timeScale = 1f;
            GameplayMenuManager.Instance.HideAllSubmenus();
        }

        public void Update()
        {
            // No update logic needed for pause state (input is handled via UI or InputManager)
        }
    }
}
