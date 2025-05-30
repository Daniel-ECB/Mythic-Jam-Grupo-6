using MythicGameJam.UI.Menus;
using UnityEngine;
using System;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class PauseState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly Action _onEnterMenu;

        public PauseState(GameManager gameManager, Action onEnterMenu = null)
        {
            _gameManager = gameManager;
            _onEnterMenu = onEnterMenu;
        }

        public void Enter()
        {
            _gameManager.IsPaused = true;
            Time.timeScale = 0f;

            if (_onEnterMenu != null)
            {
                _onEnterMenu.Invoke();
            }
            else
            {
                GameplayMenuManager.Instance.ShowSubmenu(GameplayMenuManager.SubmenuType.PauseMenu);
            }
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
