using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class LoadingState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly string _targetScene;
        private AsyncOperation _loadOperation;

        public LoadingState(GameManager gameManager, string targetScene)
        {
            _gameManager = gameManager;
            _targetScene = targetScene;
        }

        public void Enter()
        {
            _gameManager.StartCoroutine(LoadSceneAsync());
        }

        public void Exit()
        {
            // Optionally: cleanup or hide loading UI
        }

        public void Update()
        {
            // Optionally: update loading UI/progress bar
        }

        private IEnumerator LoadSceneAsync()
        {
            // Optionally: show loading UI here

            _loadOperation = SceneManager.LoadSceneAsync(_targetScene);

            while (!_loadOperation.isDone)
            {
                // Optionally: update loading UI/progress bar here
                yield return null;
            }

            // After loading, transition to the next state (e.g., Gameplay)
            _gameManager.StartGameplay();
        }
    }
}
