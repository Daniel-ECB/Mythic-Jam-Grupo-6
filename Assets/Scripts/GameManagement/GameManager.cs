using MythicGameJam.Core.Utils;
using UnityEngine;
using System;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class GameManager : Singleton<GameManager>
    {
        private IGameState _currentState;

        // State references
        private HomeScreenState _homeScreenState;
        private GameplayState _gameplayState;
        private LoadingState _loadingState;

        private bool _isPaused = false;

        [SerializeField]
        private string _debugCurrentState; // This is only for debug purposes, can be removed in production

        public bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }

        protected override void Awake()
        {
            base.Awake();

            // Instantiate states (pass GameManager or other dependencies as needed)
            _homeScreenState = new HomeScreenState(this);
            _gameplayState = new GameplayState(this);

            ChangeState(_homeScreenState);
        }

        private void Update()
        {
            _currentState?.Update();
        }

        private void ChangeState(IGameState newState)
        {
            if (_currentState == newState)
                return;

            _currentState?.Exit();
            _currentState = newState;
            _debugCurrentState = newState.GetType().Name;
            _currentState?.Enter();
        }

        public void PauseGame(Action onEnterMenu = null)
        {
            Action menuAction = onEnterMenu ?? (() => MythicGameJam.UI.Menus.GameplayMenuManager.Instance.ShowSubmenu(
                MythicGameJam.UI.Menus.GameplayMenuManager.SubmenuType.PauseMenu));
            ChangeState(new PauseState(this, menuAction));
        }

        public void GoToHomeScreen() => ChangeState(_homeScreenState);
        public void StartGameplay() => ChangeState(_gameplayState);

        public void LoadScene(string sceneName, System.Action onLoaded)
        {
            _loadingState = new LoadingState(this, sceneName, onLoaded);
            ChangeState(_loadingState);
        }
    }
}
