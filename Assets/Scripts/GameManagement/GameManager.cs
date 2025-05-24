using UnityEngine;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private IGameState _currentState;

        // State references
        private HomeScreenState _homeScreenState;
        private GameplayState _gameplayState;
        private PauseState _pauseState;
        private LoadingState _loadingState;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Instantiate states (pass GameManager or other dependencies as needed)
            _homeScreenState = new HomeScreenState(this);
            _gameplayState = new GameplayState(this);
            _pauseState = new PauseState(this);

            ChangeState(_homeScreenState);
        }

        private void Update()
        {
            _currentState?.Update();
        }

        public void ChangeState(IGameState newState)
        {
            if (_currentState == newState)
                return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void GoToHomeScreen() => ChangeState(_homeScreenState);
        public void StartGameplay() => ChangeState(_gameplayState);
        public void PauseGame() => ChangeState(_pauseState);

        public void LoadScene(string sceneName)
        {
            _loadingState = new LoadingState(this, sceneName);
            ChangeState(_loadingState);
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
